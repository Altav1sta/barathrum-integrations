using Barathrum.GamesSync.Steam.Models;
using System.Net.Http.Json;

namespace Barathrum.GamesSync.Steam
{
    internal class SteamClient(HttpClient httpClient, SteamConfig config)
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly SteamConfig config = config;

        public async Task<GetOwnedGamesResponse> GetPaidGamesList(string steamId)
        {
            const string urlFormat = "IPlayerService/GetOwnedGames/v0001?key={0}&steamid={1}&format=json&include_appinfo=1&include_played_free_game=0";

            var response = await httpClient.GetAsync(string.Format(urlFormat, config.ApiKey, steamId));

            response.EnsureSuccessStatusCode();

            var model = await response.Content.ReadFromJsonAsync<SteamApiResponse<GetOwnedGamesResponse>>()
                ?? throw new Exception("Can't deserialize response to model");

            return model.Response;
        }
    }
}
