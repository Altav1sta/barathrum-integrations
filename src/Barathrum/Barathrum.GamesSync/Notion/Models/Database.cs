namespace Barathrum.GamesSync.Notion.Models
{
    internal class Database
    {
        public required string @object { get; init; }

        public required Guid id { get; init; }

        public DateTime? created_time { get; init; }

        public User? created_by { get; init; }

        public DateTime? last_edited_time { get; init; }

        public User? last_edited_by { get; init; }

        public RichTextObject[]? title { get; init; }

        public RichTextObject[]? description { get; init; }

        public FileObject? icon { get; init; }

        public FileObject? cover { get; init; }

        public required Dictionary<string, DatabaseProperty> properties { get; init; }

        public ParentObject? parent { get; init; }

        public string? url { get; init; }

        public bool? archived { get; init; }

        public bool? in_trash { get; init; }

        public bool? is_inline { get; init; }

        public string? public_url { get; init; }
    }
}
