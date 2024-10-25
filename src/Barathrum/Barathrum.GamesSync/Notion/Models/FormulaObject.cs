using Barathrum.GamesSync.Notion.Enums;

namespace Barathrum.GamesSync.Notion.Models
{
    internal class FormulaObject
    {
        public required FormulaObjectType type { get; init; }
        public bool? boolean { get; init; }
        public DateTime? date { get; init; }
        public int? number { get; init; }
        public string? @string { get; init; }
    }
}
