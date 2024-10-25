using Barathrum.GamesSync.Notion.Enums;

namespace Barathrum.GamesSync.Notion.Models
{
    internal class User
    {
        public required string @object { get; init; }
        public required Guid id { get; init; }
        public UserType? type { get; init; }
        public string? name { get; init; }
        public string? avatar_url { get; init; }
        public Person? person { get; init; }
        public Bot? bot { get; init; }
    }
}
