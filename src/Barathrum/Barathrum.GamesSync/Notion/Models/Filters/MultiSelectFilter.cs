namespace Barathrum.GamesSync.Notion.Models.Filters
{
    internal class MultiSelectFilter
    {
        public string? contains { get; init; }
        public string? does_not_contain { get; init; }
        public bool? is_empty { get; init; }
        public bool? is_not_empty { get; init; }
    }
}
