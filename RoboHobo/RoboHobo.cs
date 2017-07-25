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
            registerSadReaction();
            registerHappyReaction();
            registerGoodJobReaction();
            registerToddReaction();
            registerKantaiReaction();
            registerNopeDog();

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzM5MTMyNTgxNzgwMTI3NzQ1.DFfjBg.7ti7r5WzQZgrwUT4LhHOln7VOwM", TokenType.Bot);
            });
        }

        private void registerCommands()
        {
            commands.CreateCommand("commands")
                .Do(async (e) =>
                {
                    String[] commandList = new String[] { "fbi", "lenny", "whenispax", "sad", "nopedog", "happy", "goodjob", "todd", "kancolle" };
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

        private void registerSadReaction()
        {
            commands.CreateCommand("sad")
                .Do(async (e) =>
                {
                    String[] sadReactions = new String[] { "sad1.jpg", "sad2.gif", "sad3.png", "sad4.jpg", "sad5.jpg", "sad6.gif", "sad7.gif", "sad8.gif", "sad9.jpg", "sad10.gif"};
                    await e.Channel.SendFile("images/sad/" + sadReactions[r.Next(sadReactions.Length)]);
                });
        }

        private void registerNopeDog()
        {
            commands.CreateCommand("nopedog")
                .Do(async (e) =>
                {
                    await e.Channel.SendFile("images/nopedog.gif");
                });
        }

        private void registerHappyReaction()
        {
            commands.CreateCommand("happy")
                .Do(async (e) =>
                {
                    String[] happyReactions = new String[] { "happy1.jpg", "happy2.jpg", "happy3.jpg", "happy4.png", "happy5.gif", "happy6.png", "happy7.jpg", "happy8.gif", "happy9.jpg", "happy10.jpg" };
                    await e.Channel.SendFile("images/happy/" + happyReactions[r.Next(happyReactions.Length)]);
                });
        }

        private void registerGoodJobReaction()
        {
            commands.CreateCommand("goodjob")
                .Do(async (e) =>
                {
                    String[] goodjobReactions = new String[] { "goodjob1.gif", "goodjob2.jpg", "goodjob3.gif", "goodjob4.gif", "goodjob5.png", "goodjob6.jpg", "goodjob7.gif", "goodjob8.jpg", "goodjob9.gif", "goodjob10.png", "goodjob11.gif" };
                    await e.Channel.SendFile("images/goodjob/goodjob1.gif");
                    //await e.Channel.SendFile("images/goodjob/" + goodjobReactions[r.Next(goodjobReactions.Length)]);
                });
        }

        private void registerToddReaction()
        {
            commands.CreateCommand("todd")
                .Do(async (e) =>
                {
                    String[] toddReactions = new String[] { "todd1.jpg", "todd2.jpg", "todd3.jpg", "todd4.jpg", "todd5.jpg", "todd6.png", "todd7.png" };
                    await e.Channel.SendFile("images/todd/" + toddReactions[r.Next(toddReactions.Length)]);
                });
        }

        private void registerKantaiReaction()
        {
            commands.CreateCommand("kancolle")
                .Do(async (e) =>
                {
                    String[] kantaiReactions = new String[] { "kantai1.gif", "kantai2.gif", "kantai3.gif", "kantai4.gif", "kantai5.gif", "kantai6.gif", "kantai7.gif", "kantai8.gif", "kantai9.gif", "kantai10.gif" };
                    await e.Channel.SendFile("images/kancolle/" + kantaiReactions[r.Next(kantaiReactions.Length)]);
                });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
