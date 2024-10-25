namespace Barathrum.GamesSync.Notion.Models
{
    internal class Page
    {
        public string? @object { get; init; }
        public string? id { get; init; }
        public DateTime? created_time { get; init; }
        public User? created_by { get; init; }
        public DateTime? last_edited_time { get; init; }
        public User? last_edited_by { get; init; }
        public bool? archived { get; init; }
        public bool? in_trash { get; init; }
        public FileObject? icon { get; init; }
        public FileObject? cover { get; init; }
        public Dictionary<string, PageProperty>? properties { get; init; }
        public ParentObject? parent { get; init; }
        public string? url { get; init; }
        public string? @string { get; init; }
    }
}
