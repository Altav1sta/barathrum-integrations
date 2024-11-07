namespace Barathrum.GamesSync.Notion.Models.Filters
{
    internal class FormulaFilter
    {
        public CheckboxFilter? checkbox { get; init; }
        public DateFilter? date { get; init; }
        public NumberFilter? number { get; init; }
        public RichTextFilter? @string { get; init; }
    }
}
