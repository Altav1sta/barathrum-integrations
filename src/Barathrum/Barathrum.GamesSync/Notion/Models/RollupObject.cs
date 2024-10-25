using Barathrum.GamesSync.Notion.Enums;

namespace Barathrum.GamesSync.Notion.Models
{
    internal class RollupObject
    {
        public RollupObjectType? type { get; init; }
        public string? function { get; init; }
        public object? array { get; init; }
        public object? date { get; init; }
        public object? incomplete { get; init; }
        public int? number { get; init; }
        public object? unsupported { get; init; }
    }
}
