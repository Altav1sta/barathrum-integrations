namespace Barathrum.GamesSync.Notion.Models.Filters
{
    internal class SelectFilter
    {
        public string? equals { get; init; }
        public string? does_not_equal { get; init; }
        public bool? is_empty { get; init; }
        public bool? is_not_empty { get; init; }
    }
}
