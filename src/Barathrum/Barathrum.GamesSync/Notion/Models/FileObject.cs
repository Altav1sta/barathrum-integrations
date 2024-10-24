using Barathrum.GamesSync.Notion.Enums;

namespace Barathrum.GamesSync.Notion.Models
{
    internal class FileObject
    {
        public required FileObjectType type { get; init; }
        public NotionFile? file { get; init; }
        public ExternalFile? external { get; init; }
        public string? emoji { get; init; }
    }
}
