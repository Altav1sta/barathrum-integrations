using System.Text.Json.Serialization;

namespace Barathrum.GamesSync.Steam.Models
{
    internal class Player
    {
        [JsonPropertyName("steamid")]
        public required string SteamId { get; init; }

        [JsonPropertyName("personaname")]
        public required string Name { get; init; }

        [JsonPropertyName("profileurl")]
        public required string ProfileUrl { get; init; }

        [JsonPropertyName("avatar")]
        public required string AvatarUrl { get; init; }

        [JsonPropertyName("avatarmedium")]
        public required string AvatarMediumUrl { get; init; }

        [JsonPropertyName("avatarfull")]
        public required string AvatarFullUrl { get; init; }
    }
}
