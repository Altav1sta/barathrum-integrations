namespace Barathrum.GamesSync.Notion.Models
{
    internal class Relation
    {
        public required Guid database_id { get; init; }
        public required string synced_property_id { get; init; }
        public required string synced_property_name { get; init; }
    }
}
