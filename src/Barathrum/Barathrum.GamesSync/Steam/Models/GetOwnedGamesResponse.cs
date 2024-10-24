using System.Text.Json.Serialization;

namespace Barathrum.GamesSync.Steam.Models
{
    internal class GetOwnedGamesResponse
    {
        [JsonPropertyName("game_count")]
        public int GameCount { get; init; }

        [JsonPropertyName("games")]
        public Game[] Games { get; init; } = [];
    }
}
