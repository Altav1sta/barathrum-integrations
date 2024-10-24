namespace Barathrum.GamesSync.Notion.Models
{
    internal class NotionFile
    {
        public required string url { get; init; }
        public required DateTime expiry_time { get; init; }
    }
}
