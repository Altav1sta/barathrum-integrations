using Barathrum.GamesSync.Notion.Enums;
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


        public async Task<Page> CreatePage(Dictionary<string, PageProperty> properties)
        {
            const string url = "v1/pages";

            var parent = new ParentObject
            {
                type = ParentObjectType.database_id,
                database_id = config.DatabaseId
            };

            var response = await httpClient.PostAsJsonAsync(url, new { parent, properties }, JsonOptions);
            var result = await response.GetFromResponse<Page>(JsonOptions);

            return result;
        }

        public async Task<Database> GetDatabase()
        {
            const string urlFormat = "v1/databases/{0}";

            var response = await httpClient.GetAsync(string.Format(urlFormat, config.DatabaseId));
            var result = await response.GetFromResponse<Database>(JsonOptions);

            return result;
        }

        public async Task<PagesList> QueryDatabase(string[]? filterProperties = null, FilterObject? filter = null, SortObject[]? sorts = null)
        {
            const string urlFormat = "v1/databases/{0}/query/{1}";

            var queryParams = filterProperties == null
                ? ""
                : $"filter_properties={string.Join("&filter_properties=", filterProperties)}";

            var response = await httpClient.PostAsJsonAsync(string.Format(urlFormat, config.DatabaseId, queryParams), new { filter, sorts }, JsonOptions);
            var result = await response.GetFromResponse<PagesList>(JsonOptions);

            return result;
        }
    }
}
