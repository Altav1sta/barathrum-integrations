using Barathrum.GamesSync.Notion.Enums;

namespace Barathrum.GamesSync.Notion.Models
{
    internal class ParentObject
    {
        public required ParentObjectType type { get; init; }
        public Guid? database_id { get; init; }
        public Guid? page_id { get; init; }
        public Guid? block_id { get; init; }
        public bool? workspace { get; init; }
    }
}
