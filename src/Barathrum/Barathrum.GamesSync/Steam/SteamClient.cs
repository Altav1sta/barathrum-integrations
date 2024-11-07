using Barathrum.GamesSync.Steam.Models;
using Barathrum.GamesSync.Steam.Models.Response;

namespace Barathrum.GamesSync.Steam
{
    internal class SteamClient(HttpClient httpClient, SteamConfig config)
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly SteamConfig config = config;

        public async Task<Game[]> GetGamesList(string steamId, bool includeFreeGames)
        {
            const string urlFormat = "IPlayerService/GetOwnedGames/v0001?key={0}&steamid={1}&format=json&include_appinfo=1&include_played_free_game={2}";

            var response = await httpClient.GetAsync(string.Format(urlFormat, config.ApiKey, steamId, Convert.ToInt32(includeFreeGames)));
            var result = await response.GetFromResponse<SteamApiResponse<GetOwnedGamesResponse>>();

            return result.Response.Games;
        }

        public async Task<Player[]> GetPlayerSummaries(ICollection<string> steamIds)
        {
            const string urlFormat = "ISteamUser/GetPlayerSummaries/v0002?key={0}&steamids={1}&format=json";

            var response = await httpClient.GetAsync(string.Format(urlFormat, config.ApiKey, string.Join(",", steamIds)));
            var result = await response.GetFromResponse<SteamApiResponse<GetPlayerSummariesResponse>>();

            return result.Response.Players;
        }
    }
}
