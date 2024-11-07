namespace Barathrum.GamesSync.Notion.Models
{
    internal class SortObject
    {
        public string? property { get; init; }
        public string? timestamp { get; init; }
        public required string direction { get; init; }
    }
}
