using Barathrum.GamesSync.Notion.Enums;

namespace Barathrum.GamesSync.Notion.Models
{
    internal class SelectOption
    {
        public required string id { get; init; }
        public required string name { get; init; }
        public required Color color { get; init; }
    }
}
