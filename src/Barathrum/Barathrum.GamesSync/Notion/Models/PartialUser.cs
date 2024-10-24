namespace Barathrum.GamesSync.Notion.Models
{
    internal class PartialUser
    {
        public required string @object { get; init; }

        public required Guid id { get; init; }
    }
}
