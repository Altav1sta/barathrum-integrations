using Barathrum.GamesSync.Notion.Enums;

namespace Barathrum.GamesSync.Notion.Models
{
    internal class DatabaseProperty
    {
        public required string id { get; init; }
        public required DatabasePropertyType type { get; init; }
        public string? name { get; init; }
        public string? description { get; init; }
        public object? checkbox { get; init; }
        public object? created_by { get; init; }
        public object? created_time { get; init; }
        public object? date { get; init; }
        public object? email { get; init; }
        public object? files { get; init; }
        public Equation? formula { get; init; }
        public object? last_edited_by { get; init; }
        public object? last_edited_time { get; init; }
        public Select? multi_select { get; init; }
        public Number? number { get; init; }
        public object? people { get; init; }
        public object? phone_number { get; init; }
        public Relation? relation { get; init; }
        public object? rich_text { get; init; }
        public Rollup? rollup { get; init; }
        public Select? select { get; init; }
        public Status? status { get; init; }
        public object? title { get; init; }
        public object? url { get; init; }
    }
}
