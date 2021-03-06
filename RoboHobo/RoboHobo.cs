﻿using Discord;
using Discord.Commands;
using GiphyDotNet;
using RestSharp;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboHobo
{
    class RoboHobo
    {
        DiscordClient discord;
        CommandService commands;
        Random r;
        

        public RoboHobo()
        {
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '~';
                x.AllowMentionPrefix = true;
            });

            commands = discord.GetService<CommandService>();
            r = new Random();

            registerCommands();
            registerFbi();
            registerLenny();
            registerWhenIsPax();
            registerReaction();
            registerRandomGif();
            registerAnimeGif();
            registerCommandAlias();

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect(ConfigurationManager.AppSettings["discordKey"], TokenType.Bot);
            });
        }

        private void registerCommands()
        {
            commands.CreateCommand("commands")
                .Do(async (e) =>
                {
                    String[] commandList = new String[] { "fbi", "lenny", "whenispax", "react", "randomgif", "alias", "animegif"};
                    String commands = "";
                    foreach(String command in commandList)
                    {
                        commands = commands + "~" + command + "\n";
                    }
                   await e.Channel.SendMessage("The current list of commands are: \n" + commands);
                });
        }

        private void registerFbi()
        {
            commands.CreateCommand("fbi")
                .Do(async (e) =>
                {
                    await e.Channel.SendFile("images/fbi1.gif");
                });
        }

        private void registerLenny()
        {
            commands.CreateCommand("lenny")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("( ͡° ͜ʖ ͡°)");
                });
        }

        private void registerWhenIsPax()
        {
            commands.CreateCommand("whenispax")
                .Do(async (e) =>
                {
                    DateTime paxStart = new DateTime(2017, 9, 2, 8 ,0 ,0);
                    DateTime currentTime = DateTime.Now;
                    var message = "";
                    if(paxStart.CompareTo(currentTime) > 0)
                    {
                        TimeSpan remainingTime = paxStart.Subtract(currentTime);
                        message = remainingTime.Days + " days, " + remainingTime.Hours + " hours, " + remainingTime.Minutes + " minutes, and " + remainingTime.Seconds + " seconds until PAX West 2017!";
                    } else
                    {
                        message = "PAX West 2017 is over!";
                    }

                    await e.Channel.SendMessage(message);
                });
        }

        private void registerReaction()
        {
            commands.CreateCommand("react")
                .Alias(new String[] { "r", "reaction" })
                .Description("Send a reaction image/gif")
                .Parameter("reactionType", Discord.Commands.ParameterType.Required)
                .Do(async (e) =>
                {
                    String rType = e.GetArg("reactionType").ToString().ToLower();
                    switch(rType)
                    {
                        case "help":
                            await e.Channel.SendMessage("The current list of reaction types are: \n happy, sad, goodjob, todd, kancolle");
                            break;
                        case "sad":
                            String[] sadReactions = new String[] { "sad1.jpg", "sad2.gif", "sad3.png", "sad4.jpg", "sad5.jpg", "sad6.gif", "sad7.gif", "sad8.gif", "sad9.jpg", "sad10.gif" };
                            await e.Channel.SendFile("images/sad/" + sadReactions[r.Next(sadReactions.Length)]);
                            break;
                        case "goodjob":
                            String[] goodjobReactions = new String[] { "goodjob1.gif", "goodjob2.jpg", "goodjob3.gif", "goodjob4.gif", "goodjob5.png", "goodjob6.jpg", "goodjob7.gif", "goodjob8.jpg", "goodjob9.gif", "goodjob10.png", "goodjob11.gif" };
                            await e.Channel.SendFile("images/goodjob/" + goodjobReactions[r.Next(goodjobReactions.Length)]);
                            break;
                        case "happy":
                            String[] happyReactions = new String[] { "happy1.jpg", "happy2.jpg", "happy3.jpg", "happy4.png", "happy5.gif", "happy6.png", "happy7.jpg", "happy8.gif", "happy9.jpg", "happy10.jpg" };
                            await e.Channel.SendFile("images/happy/" + happyReactions[r.Next(happyReactions.Length)]);
                            break;
                        case "todd":
                            String[] toddReactions = new String[] { "todd1.jpg", "todd2.jpg", "todd3.jpg", "todd4.jpg", "todd5.jpg", "todd6.png", "todd7.png" };
                            await e.Channel.SendFile("images/todd/" + toddReactions[r.Next(toddReactions.Length)]);
                            break;
                        case "kancolle":
                            String[] kantaiReactions = new String[] { "kantai1.gif", "kantai2.gif", "kantai3.gif", "kantai4.gif", "kantai5.gif", "kantai6.gif", "kantai7.gif", "kantai8.gif", "kantai9.gif", "kantai10.gif" };
                            await e.Channel.SendFile("images/kancolle/" + kantaiReactions[r.Next(kantaiReactions.Length)]);
                            break;
                        default:
                            await e.Channel.SendMessage("Sorry, the reaction type \"" + e.GetArg("reactionType").ToString() + "\" does not exist! Type \"~react help\" for a list of all reaction types!");
                            break;
                    }
                });
        }

        private void registerRandomGif()
        {
            commands.CreateCommand("randomgif")
                .Alias(new String[] { "random", "rgif" })
                .Description("Send a random GIPHY gif to the chat!")
                .Do(async (e) =>
                {
                    GiphyApi gifSearch = new GiphyApi(ConfigurationManager.AppSettings["giphyKey"]);
                    await e.Channel.SendMessage(gifSearch.GetRandomGif()); 
                });
        }

        private void registerAnimeGif()
        {
            commands.CreateCommand("animegif")
                .Alias(new String[] { "agif", "anime" })
                .Description("Send an anime related searh result gif")
                .Parameter("searchQuery", Discord.Commands.ParameterType.Required)
                .Do(async (e) =>
                 {
                     var filteredQuery = e.GetArg("searchQuery").ToString().Trim().ToLower().Replace("_", "+");
                     Console.WriteLine(filteredQuery);
                     GiphyApi animeGifSearch = new GiphyApi(ConfigurationManager.AppSettings["giphyKey"]);
                     await e.Channel.SendMessage(animeGifSearch.GetAnimeGif(filteredQuery));
                 });
        }

        private void registerCommandAlias()
        {
            commands.CreateCommand("alias")
                .Do(async (e) =>
                 {
                     await e.Channel.SendMessage("The current commands have the following aliases (i.e. you can call the command with the alias):\n ~react: r, reaction\n ~randomgif: random, rgif");
                 });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
