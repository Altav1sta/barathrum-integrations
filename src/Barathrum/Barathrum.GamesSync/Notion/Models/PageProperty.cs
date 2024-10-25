using Barathrum.GamesSync.Notion.Enums;

namespace Barathrum.GamesSync.Notion.Models
{
    internal class PageProperty
    {
        public string? id { get; init; }
        public PropertyType? type { get; init; }
        public bool? checkbox { get; init; }
        public User? created_by { get; init; }
        public DateTime? created_time { get; init; }
        public Date? date { get; init; }
        public string? email { get; init; }
        public FileObject[]? files { get; init; }
        public FormulaObject? formula { get; init; }
        public bool? has_more { get; init; }
        public User? last_edited_by { get; init; }
        public DateTime? last_edited_time { get; init; }
        public SelectOption[]? multi_select { get; init; }
        public int? number { get; init; }
        public User[]? people { get; init; }
        public string? phone_number { get; init; }
        public Page[]? relation { get; init; }
        public RichTextObject[]? rich_text { get; init; }
        public RollupObject? rollup { get; init; }
        public SelectOption? select { get; init; }
        public SelectOption? status { get; init; }
        public RichTextObject[]? title { get; init; }
        public string? url { get; init; }
        public UniqueId? unique_id { get; init; }
        public Verification? verification { get; init; }
    }
}
