namespace Barathrum.GamesSync.Notion.Models
{
    internal class ObjectList<T>
    {
        public string? @object { get; init; }
        public T[]? results { get; init; }
        public string? next_cursor { get; init; }
        public bool? has_more { get; init; }
        public string? type { get; init; }
        public object? page_or_database { get; init; }
        public object? block { get; init; }
    }
}
