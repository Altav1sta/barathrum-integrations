using System.Net.Http.Json;
using System.Text.Json;

namespace Barathrum.GamesSync
{
    internal static class Extensions
    {
        public static async Task<T> GetFromResponse<T>(this HttpResponseMessage response, JsonSerializerOptions? jsonOptions = null)
        {
            try
            {
                response.EnsureSuccessStatusCode();

                var model = await response.Content.ReadFromJsonAsync<T>(jsonOptions)
                    ?? throw new Exception("Can't deserialize response to model");

                return model;
            }
            catch
            {
                var resultView = await response.Content.ReadAsStringAsync();
                Console.WriteLine(resultView);
                throw;
            }
        }
    }
}
