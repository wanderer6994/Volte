﻿using System.Threading.Tasks;
using Qmmands;
using Volte.Extensions;

namespace Volte.Commands.Modules.Admin
{
    public partial class AdminModule
    {
        [Command("Bin")]
        [Description("Create a debug dump on bin.greemdev.net with debug information used for Support.")]
        [Remarks("Usage: |prefix|bin")]
        public async Task BinAsync()
        {
            await Context.CreateEmbed(
                    "Take this URL to [Volte's Support Discord](https://greemdev.net/Discord) for support with this bot." +
                    "\n" +
                    "\n" +
                    $"https://bin.greemdev.net/{BinService.Execute(Db.GetData(Context.Guild))}")
                .SendToAsync(Context.Channel);
        }
    }
}