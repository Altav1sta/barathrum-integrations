namespace Barathrum.GamesSync.Notion.Models.Filters
{
    internal class RichTextFilter
    {
        public string? contains { get; init; }
        public string? does_not_contain { get; init; }
        public string? does_not_equal { get; init; }
        public string? ends_with { get; init; }
        public string? equals { get; init; }
        public bool? is_empty { get; init; }
        public bool? is_not_empty { get; init; }
        public string? starts_with { get; init; }
    }
}
