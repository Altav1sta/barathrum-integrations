using Barathrum.GamesSync.Notion.Enums;

namespace Barathrum.GamesSync.Notion.Models.Filters
{
    internal class TimestampFilter
    {
        public required TimestampType timestamp { get; init; }
        public DateFilter? created_time { get; init; }
        public DateFilter? last_edited_time { get; init; }
    }
}
