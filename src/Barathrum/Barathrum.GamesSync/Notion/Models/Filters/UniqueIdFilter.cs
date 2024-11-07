namespace Barathrum.GamesSync.Notion.Models.Filters
{
    internal class UniqueIdFilter
    {
        public int? does_not_equal { get; init; }
        public int? equals { get; init; }
        public int? greater_than { get; init; }
        public int? greater_than_or_equal_to { get; init; }
        public int? less_than { get; init; }
        public int? less_than_or_equal_to { get; init; }
    }
}
