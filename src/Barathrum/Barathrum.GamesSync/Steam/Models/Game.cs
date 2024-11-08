using System.Text.Json.Serialization;

namespace Barathrum.GamesSync.Steam.Models
{
    internal class Game
    {
        [JsonPropertyName("appid")]
        public int AppId { get; init; }

        [JsonPropertyName("name")]
        public string? Name { get; init; }

        [JsonPropertyName("playtime_2weeks")]
        public int? Playtime2Weeks { get; init; }

        [JsonPropertyName("playtime_forever")]
        public int? PlaytimeForever { get; init; }

        [JsonPropertyName("img_icon_url")]
        public string? ImageIconUrl { get; init; }

        [JsonPropertyName("has_community_visible_stats")]
        public bool? HasCommunityVisibleStats { get; init; }

        [JsonPropertyName("content_descriptorids")]
        public int[]? ContentDesciptorIds { get; init; }


        [JsonIgnore]
        public bool IsFreeToPlay { get; set; }
    }
}
