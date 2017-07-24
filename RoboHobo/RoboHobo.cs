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

        public RoboHobo()
        {
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '/';
                x.AllowMentionPrefix = true;
            });

            var commands = discord.GetService<CommandService>();

            commands.CreateCommand("Hello")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Hi!");
                });

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzM4OTM0MDA3ODAwNTI4ODk5.DFcvQg.lDurq8F8mcRyfEOxZk-rDCkjZis", TokenType.Bot);
            });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
