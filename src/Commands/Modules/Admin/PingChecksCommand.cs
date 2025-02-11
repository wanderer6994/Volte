﻿using System.Threading.Tasks;
using Qmmands;
using Volte.Commands.Preconditions;
using Volte.Extensions;

namespace Volte.Commands.Modules.Admin
{
    public partial class AdminModule : VolteModule
    {
        [Command("PingChecks")]
        [Description("Enable/Disable checking for @everyone and @here for this guild.")]
        [Remarks("Usage: |prefix|pingchecks {true|false}")]
        [RequireGuildAdmin]
        public async Task PingChecksAsync(bool enabled)
        {
            var data = Db.GetData(Context.Guild);
            data.Configuration.Moderation.MassPingChecks = enabled;
            Db.UpdateData(data);
            await Context
                .CreateEmbed(enabled ? "MassPingChecks has been enabled." : "MassPingChecks has been disabled.")
                .SendToAsync(Context.Channel);
        }
    }
}