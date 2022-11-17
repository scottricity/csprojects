using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Discord;
using Discord.Net;
using Discord.WebSocket;
using Discord.Interactions;
using Discord.Interactions.Builders;
using System.Text.Json;
using System.IO;
using NASA;

public class Program
{

    

    private string getToken()
    {
        return File.ReadAllText(@"./token.txt") ?? "n/a";
    }
    public static Task Main(string[] args) => new Program().MainAsync();

    private DiscordSocketClient? _client;
    public async Task MainAsync()
    {
        _client = new DiscordSocketClient(config: new DiscordSocketConfig { GatewayIntents = GatewayIntents.All });

        _client.Log += Log;

        _client.Ready += Client_Ready;

        _client.SlashCommandExecuted += SlashCommandHandler;

        //This is usually not a safe way.
        var token = getToken();

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync(); //Start the bot

        await Task.Delay(-1);
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    public class Embed : object, Discord.IEmbed
    {
        public EmbedAuthor? Author {get;}
        public string Description { get; internal set; }
        public string Url { get; internal set; }
        public string Title { get; internal set; }
        public DateTimeOffset? Timestamp { get; internal set; }
        public Color? Color { get; internal set; }
        public EmbedImage? Image { get; internal set; }
        public EmbedVideo? Video { get; internal set; }
        public EmbedFooter? Footer { get; internal set; }
        public EmbedProvider? Provider { get; internal set; }
 
        public EmbedThumbnail? Thumbnail { get; internal set; }
        public ImmutableArray<EmbedField>? Fields {get;}

        public EmbedType Type => throw new NotImplementedException();

        ImmutableArray<EmbedField> IEmbed.Fields => throw new NotImplementedException();
    }



    private async Task SlashCommandHandler(SocketSlashCommand command)
    {
        if (command != null)
        {
            if (command.Data.Name == "ping")
            {
                await command.RespondAsync("Pong!");
            }else if(command.Data.Name == "nasa")
            {
                
                var l = new NASA_API();
                
                var membed = new Discord.EmbedBuilder();
                await l.makeRequest("https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY");
                membed.Description = "ye";
                await command.RespondAsync(embed: membed.Build());
            }
        }
    }

    public async Task InstallCommands()
    {
        if (_client == null) return;
        var guild = _client.GetGuild(1041166655709003818);

        var installList = new List<Discord.SlashCommandBuilder>();

        //Ping Command
        var pingCommand = new Discord.SlashCommandBuilder();
        pingCommand.WithName("ping");
        pingCommand.WithDescription("Ping the majestic PieBot!");
        installList.Add(pingCommand);

        //Nasa Command
        var nasaCommand = new Discord.SlashCommandBuilder();
        nasaCommand.WithName("nasa");
        nasaCommand.WithDescription("Placeholder");
        installList.Add(nasaCommand);

        try
        {

            installList.ForEach(async cmd => {
                await _client.CreateGlobalApplicationCommandAsync(cmd.Build());
                Console.WriteLine($"Installed Command < {cmd.Name} >");
            });

        }
        catch (HttpException exception)
        {

            var json = JsonSerializer.Serialize(exception.Data);

            Console.WriteLine(json);
        }
    }
    public async Task Client_Ready()
    {
        await InstallCommands();
    }
}