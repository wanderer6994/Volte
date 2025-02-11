using System.Linq;
using System.Threading.Tasks;
using Gommon;
using Qmmands;
using Volte.Commands.Preconditions;
using Volte.Data.Models.Guild;
using Volte.Extensions;

namespace Volte.Commands.Modules.Admin
{
    public partial class AdminModule : VolteModule
    {
        [Command("TagCreate", "TagAdd", "TagNew")]
        [Priority(1)]
        [Description("Creates a tag with the specified name and response.")]
        [Remarks("Usage: |prefix|tagcreate {name} {response}")]
        [RequireGuildAdmin]
        public async Task TagCreateAsync(string name, [Remainder] string response)
        {
            var data = Db.GetData(Context.Guild);
            var tag = data.Extras.Tags.FirstOrDefault(t => t.Name.EqualsIgnoreCase(name));
            if (tag != null)
            {
                var user = Context.Client.GetUser(tag.CreatorId);
                await Context
                    .CreateEmbed(
                        $"Cannot make the tag **{tag.Name}**, as it already exists and is owned by {user.Mention}.")
                    .SendToAsync(Context.Channel);
                return;
            }

            var newTag = new Tag
            {
                Name = name,
                Response = response,
                CreatorId = Context.User.Id,
                GuildId = Context.Guild.Id,
                Uses = 0
            };

            data.Extras.Tags.Add(newTag);
            Db.UpdateData(data);

            await Context.CreateEmbedBuilder()
                .WithTitle("Tag Created!")
                .AddField("Name", newTag.Name)
                .AddField("Response", newTag.Response)
                .AddField("Creator", Context.User.Mention)
                .SendToAsync(Context.Channel);
        }

        [Command("TagDelete", "TagDel", "TagRem")]
        [Priority(1)]
        [Description("Deletes a tag if it exists.")]
        [Remarks("Usage: |prefix|tagdelete {name}")]
        public async Task TagDeleteAsync([Remainder] string name)
        {
            var data = Db.GetData(Context.Guild);
            var tag = data.Extras.Tags.FirstOrDefault(t => t.Name.EqualsIgnoreCase(name));
            if (tag == null)
            {
                await Context.CreateEmbed($"Cannot delete the tag **{name}**, as it doesn't exist.")
                    .SendToAsync(Context.Channel);
                return;
            }

            var user = Context.Client.GetUser(tag.CreatorId);

            data.Extras.Tags.Remove(tag);
            Db.UpdateData(data);
            await Context.CreateEmbed(
                    $"Deleted the tag **{tag.Name}**, created by " +
                    $"{(user != null ? user.Mention : $"user with ID **{tag.CreatorId}**")} with **{tag.Uses}** " +
                    $"{(tag.Uses != 1 ? "uses" : "use")}.")
                .SendToAsync(Context.Channel);
        }
    }
}