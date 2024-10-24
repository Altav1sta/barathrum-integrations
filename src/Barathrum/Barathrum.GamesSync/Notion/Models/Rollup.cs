namespace Barathrum.GamesSync.Notion.Models
{
    internal class Rollup
    {
        public required string function { get; init; }
        public required string relation_property_id { get; init; }
        public required string relation_property_name { get; init; }
        public required string rollup_property_id { get; init; }
        public required string rollup_property_name { get; init; }
    }
}
