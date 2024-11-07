using Barathrum.GamesSync.Steam.Models;

namespace Barathrum.GamesSync.Steam
{
    internal class SteamClient(HttpClient httpClient, SteamConfig config)
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly SteamConfig config = config;

        public async Task<Game[]> GetPaidGamesList(string steamId)
        {
            const string urlFormat = "IPlayerService/GetOwnedGames/v0001?key={0}&steamid={1}&format=json&include_appinfo=1&include_played_free_game=0";

            var response = await httpClient.GetAsync(string.Format(urlFormat, config.ApiKey, steamId));
            var result = await response.GetFromResponse<SteamApiResponse<GetOwnedGamesResponse>>();

            return result.Response.Games;
        }
    }
}
