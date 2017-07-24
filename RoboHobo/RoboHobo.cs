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
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
            });

            commands = discord.GetService<CommandService>();

            registerFbi();

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzM4OTM0MDA3ODAwNTI4ODk5.DFcvQg.lDurq8F8mcRyfEOxZk-rDCkjZis", TokenType.Bot);
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

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
