namespace Barathrum.GamesSync.Notion.Models.Filters
{
    internal class DateFilter
    {
        public DateTime? after { get; init; }
        public DateTime? before { get; init; }
        public DateTime? equals { get; init; }
        public bool? is_empty { get; init; }
        public bool? is_not_empty { get; init; }
        public object? next_month { get; init; }
        public object? next_week { get; init; }
        public object? next_year { get; init; }
        public DateTime? on_or_after { get; init; }
        public DateTime? on_or_before { get; init; }
        public object? past_month { get; init; }
        public object? past_week { get; init; }
        public object? past_year { get; init; }
        public object? this_week { get; init; }
    }
}
