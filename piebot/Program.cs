using System;
using System.Collections.Generic;
using Discord;
using Discord.Net;
using Discord.API;
using Discord.WebSocket;
using System.Text.Json;
using System.Text;
using System.Formats;

public class Program
{
    public static Task Main(string[] args) => new Program().MainAsync();

    private DiscordSocketClient? _client;
    public async Task MainAsync()
    {
        _client = new DiscordSocketClient(config: new DiscordSocketConfig { GatewayIntents = GatewayIntents.All });

        _client.Log += Log;

        _client.Ready += Client_Ready;

        _client.SlashCommandExecuted += SlashCommandHandler;

        //This is usually not a safe way.
        var token = "MTA0MTE1OTg1NjQ2MTE5MzI5Ng.GlCZMG.U1D5rRc2_qeKyJTjQqBskGQVCb8Fn86ITsddV0";

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync(); //Start the bot

        await Task.Delay(-1);
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }



    private async Task SlashCommandHandler(SocketSlashCommand command)
    {
        if (command != null)
        {
            if (command.Data.Name == "ping")
            {
                await command.RespondAsync("Pong!");
            }
        }
    }

    public async Task InstallCommands()
    {
        var guild = _client.GetGuild(1041166655709003818);

        var installList = new List<SlashCommandBuilder>();
        var pingCommand = new SlashCommandBuilder();
        pingCommand.WithName("ping");
        pingCommand.WithDescription("Ping the majestic PieBot!");
        installList.Add(pingCommand);

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