using System.Text.Json.Serialization;

namespace Barathrum.GamesSync.Steam.Models.Response
{
    internal class GetPlayerSummariesResponse
    {
        [JsonPropertyName("players")]
        public Player[] Players { get; init; } = [];
    }
}
