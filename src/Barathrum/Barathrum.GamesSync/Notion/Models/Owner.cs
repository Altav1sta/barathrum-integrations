using Barathrum.GamesSync.Notion.Enums;

namespace Barathrum.GamesSync.Notion.Models
{
    internal class Owner
    {
        public OwnerType? type { get; init; }
        public bool? worspace { get; init; }
        public bool? user { get; init; }
    }
}
