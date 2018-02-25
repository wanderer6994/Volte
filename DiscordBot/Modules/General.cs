﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using SIVA.Core.UserAccounts;

namespace SIVA.Modules
{
    public class General : ModuleBase<SocketCommandContext>
    {
        [Command("Stats")]
        public async Task MyStats([Remainder]string arg = "")
        {
            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;
            
            if (Context.Guild.Id == 377879473158356992)
            {
                await Context.Channel.SendMessageAsync("That command is disabled on this server.");
            }
            else
            {
                var account = UserAccounts.GetAccount(target);
                await Context.Channel.SendMessageAsync($"**{target.Username}** has {account.XP} XP and {account.Points} points.");
            }
        }

        [Command("Lenny")]
        public async Task LennyLol()
        {
            await Context.Channel.SendMessageAsync("( ͡° ͜ʖ ͡°)");
        }

        [Command("Say")]
        public async Task SayCommand([Remainder]string message)
        {
            var embed = new EmbedBuilder();
            embed.WithFooter(Utilities.GetFormattedAlert("CommandFooter", Context.User.Username));
            embed.WithDescription(message);
            embed.WithColor(new Color(Config.bot.defaultEmbedColour));


            if (Config.bot.debug == true)
            {
                Console.WriteLine("DEBUG: " + Context.User.Username + "#" + Context.User.Discriminator + " used the say command in the channel #" + Context.Channel.Name + " and said '" + message + "'!");
                await Context.Channel.SendMessageAsync("", false, embed);
            } 
            else
            {
                await Context.Channel.SendMessageAsync("", false, embed);
            }
        }

        [Command("Choose")]
        public async Task PickOne([Remainder]string message)
        {
            string[] options = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            Random r = new Random();
            string selection = options[r.Next(0, options.Length)];

            var embed = new EmbedBuilder();
            embed.WithDescription(Utilities.GetFormattedAlert("PickCommandText", selection));
            embed.WithFooter(Utilities.GetFormattedAlert("CommandFooter", Context.User.Username));
            embed.WithColor(Config.bot.defaultEmbedColour);

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("Roast")]
        public async Task Roast()
        {   //this doesnt have any other roasts as its incomplete
            await Context.Channel.SendMessageAsync(Context.User.Mention + ", maybe you would talk better if your parents were second cousins rather than first cousins.");
        }

        [Command("Info")]
        public async Task InformationCommand()
        {
            var Embed = new EmbedBuilder();
            Embed.AddField("Version", "1.0.0");
            Embed.AddField("Author", "Greem#1337");
            Embed.AddField("Language", "C# with Discord.Net");
            Embed.AddField("Server", "https://discord.io/SIVA");
            Embed.AddField("Servers", (Context.Client as DiscordSocketClient).Guilds.Count);
            Embed.AddField("Invite Me", "https://bot.discord.io/SIVA");
            Embed.AddField("Ping", (Context.Client as DiscordSocketClient).Latency);
            Embed.AddField("Client ID", "410547925597421571");
            Embed.AddField("Invite my Nadeko", "https://bot.discord.io/snadeko");
            Embed.WithThumbnailUrl("https://pbs.twimg.com/media/Cx0i4LOVQAIyLRU.png");
            Embed.WithFooter(Utilities.GetFormattedAlert("CommandFooter", Context.User.Username));
            Embed.WithColor(Config.bot.defaultEmbedColour);

            await Context.Channel.SendMessageAsync("", false, Embed);

        }

        [Command("Suggest")]
        public async Task Suggest()
        {
            await Context.Channel.SendMessageAsync("https://goo.gl/forms/i6pgYTSnDdMMNLZU2");
        }

    }
}
