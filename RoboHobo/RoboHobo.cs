using Discord;
using Discord.Commands;

using System;
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

            registerHelp();
            registerFbi();
            registerLenny();
            registerWhenIsPax();

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzM5MTMyNTgxNzgwMTI3NzQ1.DFfjBg.7ti7r5WzQZgrwUT4LhHOln7VOwM", TokenType.Bot);
            });
        }

        private void registerHelp()
        {
            commands.CreateCommand("commands")
                .Do(async (e) =>
                {
                   await e.Channel.SendMessage("The current list of commands are: \"~fbi\", \"~lenny\", \"~whenispax\"");
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
                    DateTime paxStart = new DateTime(2017, 9, 2, 8 ,0 , 0);
                    DateTime currentTime = DateTime.Now;
                    var message = "";
                    if(paxStart.CompareTo(currentTime) > 0)
                    {
                        message = currentTime.Day + " days, " + currentTime.Hour + " hours, " + currentTime.Minute + " minutes, and " + currentTime.Second + " seconds until PAX West 2017!";
                    } else
                    {
                        message = "PAX West 2017 is over!";
                    }

                    await e.Channel.SendMessage(message);
                });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
