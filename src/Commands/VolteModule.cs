using Qmmands;
using Volte.Services;

namespace Volte.Commands
{
    public abstract class VolteModule : ModuleBase<VolteContext>
    {
        public DatabaseService Db { get; set; }
        public EventService EventService { get; set; }
        public ModLogService ModLogService { get; set; }
        public CommandService CommandService { get; set; }
        public BinService BinService { get; set; }
        public EmojiService EmojiService { get; set; }
        public LoggingService Logger { get; set; }
    }
}