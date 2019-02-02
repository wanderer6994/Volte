﻿using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Volte.Helpers;
using Volte.Core.Data;
using System.Linq;
using Discord;

namespace Volte.Core.Modules.Moderation {
    public partial class ModerationModule : VolteModule {
        [Command("Kick")]
        [Summary("Kicks the given user.")]
        [Remarks("Usage: $kick {@user} [reason]")]
        public async Task Kick(SocketGuildUser user, [Remainder] string reason = "Kicked by a Moderator.") {
            if (!UserUtils.IsModerator(Context)) {
                await Context.ReactFailure();
                return;
            }

            await (await user.GetOrCreateDMChannelAsync()).SendMessageAsync(string.Empty, false,
                CreateEmbed(Context, $"You were kicked from **{Context.Guild.Name}** for **{reason}**."));

            await user.KickAsync(reason);
            await Reply(Context.Channel, CreateEmbed(Context,
                $"Successfully kicked **{Db.GetUser(user).Tag}** from this server."));
        }
    }
}