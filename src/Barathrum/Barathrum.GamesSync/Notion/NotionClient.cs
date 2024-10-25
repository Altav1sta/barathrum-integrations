using Barathrum.GamesSync.Notion.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Barathrum.GamesSync.Notion
{
    internal class NotionClient(HttpClient httpClient, NotionConfig config)
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly NotionConfig config = config;

        public readonly JsonSerializerOptions JsonOptions = new()
        {
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true
        };


        public async Task<Database> GetDatabase()
        {
            const string urlFormat = "v1/databases/{0}";

            var response = await httpClient.GetAsync(string.Format(urlFormat, config.DatabaseId));

            response.EnsureSuccessStatusCode();

            try
            {
                var model = await response.Content.ReadFromJsonAsync<Database>(JsonOptions)
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
