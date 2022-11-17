/**
using System;
using System.Collections.Generic;
using Discord.Commands;
using Discord.Commands.Builders;
using Discord;
using Discord.Net;
using Discord.WebSocket;
using System.Text.Json;

public class CommandHandler
{
    public List<SlashCommandBuilder> list = new List<SlashCommandBuilder>();
    public void AddCommand(SlashCommandBuilder cmd)
    {
        list.Add(cmd);
    } 

    public void Finished()
    {
        Console.WriteLine("Finished");
    }
}
**/