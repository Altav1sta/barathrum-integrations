namespace Barathrum.GamesSync.Notion.Models.Filters
{
    internal class NumberFilter
    {
        public int? does_not_equal { get; init; }
        public int? equals { get; init; }
        public int? greater_than { get; init; }
        public int? greater_than_or_equal_to { get; init; }
        public bool? is_empty { get; init; }
        public bool? is_not_empty { get; init; }
        public int? less_than { get; init; }
        public int? less_than_or_equal_to { get; init; }
    }
}
