﻿using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;
using Gommon;
using Qmmands;
using Volte.Commands.Preconditions;
using Volte.Extensions;

namespace Volte.Commands.Modules.Admin
{
    public partial class AdminModule : VolteModule
    {
        [Command("SelfRoleAdd", "SrA", "SrAdd")]
        [Description("Adds a role to the list of self roles for this guild.")]
        [Remarks("Usage: |prefix|selfroleadd {role}")]
        [RequireGuildAdmin]
        public async Task SelfRoleAddAsync([Remainder] SocketRole role)
        {
            var data = Db.GetData(Context.Guild);
            var target = data.Extras.SelfRoles.FirstOrDefault(x => x.EqualsIgnoreCase(role.Name));
            if (target is null)
            {
                data.Extras.SelfRoles.Add(role.Name);
                Db.UpdateData(data);
                await Context.CreateEmbed($"Successfully added **{role.Name}** to the Self Roles list for this guild.")
                    .SendToAsync(Context.Channel);
                return;
            }

            await Context.CreateEmbed($"A role with the name **{role.Name}** is already in the Self Roles list for this guild!")
                .SendToAsync(Context.Channel);

        }

        [Command("SelfRoleRemove", "SrR", "SrRem")]
        [Description("Removes a role from the list of self roles for this guild.")]
        [Remarks("Usage: |prefix|selfrole remove {role}")]
        [RequireGuildAdmin]
        public async Task SelfRoleRemoveAsync([Remainder] SocketRole role)
        {
            var data = Db.GetData(Context.Guild);

            if (data.Extras.SelfRoles.ContainsIgnoreCase(role.Name))
            {
                data.Extras.SelfRoles.Remove(role.Name);
                await Context.CreateEmbed($"Removed **{role.Name}** from the Self Roles list for this guild.")
                    .SendToAsync(Context.Channel);
                Db.UpdateData(data);
            }
            else
            {
                await Context.CreateEmbed($"The Self Roles list for this guild doesn't contain **{role.Name}**.")
                    .SendToAsync(Context.Channel);
            }
        }

        [Command("SelfRoleClear", "SrC", "SrClear", "SelfroleC")]
        [Description("Clears the self role list for this guild.")]
        [Remarks("Usage: |prefix|selfroleclear")]
        [RequireGuildAdmin]
        public async Task SelfRoleClearAsync()
        {
            var data = Db.GetData(Context.Guild);
            data.Extras.SelfRoles.Clear();
            Db.UpdateData(data);
            await Context.CreateEmbed("Successfully cleared all Self Roles for this guild.")
                .SendToAsync(Context.Channel);
        }
    }
}