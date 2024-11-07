using Barathrum.GamesSync.Notion.Models.Filters;

namespace Barathrum.GamesSync.Notion.Models
{
    internal class FilterObject
    {
        public FilterObject[]? and { get; init; }
        public FilterObject[]? or { get; init; }
        public string? property { get; init; }
        public CheckboxFilter? checkbox { get; init; }
        public DateFilter? date { get; init; }
        public FilesFilter? files { get; init; }
        public FormulaFilter? formula { get; init; }
        public MultiSelectFilter? multi_select { get; init; }
        public NumberFilter? number { get; init; }
        public SelectFilter? people { get; init; }
        public RichTextFilter? phone_number { get; init; }
        public MultiSelectFilter? relation { get; init; }
        public RichTextFilter? rich_text { get; init; }
        public SelectFilter? select { get; init; }
        public SelectFilter? status { get; init; }
        public TimestampFilter? timestamp { get; init; }
        public UniqueIdFilter? ID { get; init; }
    }
}
