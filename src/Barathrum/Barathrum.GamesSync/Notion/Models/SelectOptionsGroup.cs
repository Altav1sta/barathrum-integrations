using Barathrum.GamesSync.Notion.Enums;

namespace Barathrum.GamesSync.Notion.Models
{
    internal class SelectOptionsGroup
    {
        public required string id { get; init; }
        public required string name { get; init; }
        public required Color color { get; init; }
        public required string[] option_ids { get; init; }
    }
}
