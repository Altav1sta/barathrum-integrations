using Barathrum.GamesSync.Notion.Enums;
using Barathrum.GamesSync.Notion.Models;
using Barathrum.GamesSync.Notion.Models.Blocks;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Barathrum.GamesSync.Notion
{
    internal class NotionClient(HttpClient httpClient)
    {
        private readonly HttpClient httpClient = httpClient;

        public readonly JsonSerializerOptions JsonOptions = new()
        {
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true
        };


        public async Task<ObjectList<Block>> AppendBlockChildren<T>(string blockId, T[] children, string? after = null)
        {
            var url = $"v1/blocks/{blockId}/children";

            var response = await httpClient.PatchAsJsonAsync(url, new { children, after }, JsonOptions);
            var result = await response.GetFromResponse<ObjectList<Block>>(JsonOptions);

            return result;
        }

        public Task<Page> CreatePage(Guid databaseId, Dictionary<string, PageProperty> properties, FileObject? cover = null)
        {
            return CreatePage<object>(databaseId, properties, null, cover);
        }

        public async Task<Page> CreatePage<T>(Guid databaseId, Dictionary<string, PageProperty> properties, T[]? children, FileObject? cover = null)
        {
            const string url = "v1/pages";

            var parent = new ParentObject
            {
                type = ParentObjectType.database_id,
                database_id = databaseId
            };

            var response = await httpClient.PostAsJsonAsync(url, new { parent, properties, children, cover }, JsonOptions);
            var result = await response.GetFromResponse<Page>(JsonOptions);

            return result;
        }

        public async Task<Database> GetDatabase(Guid databaseId)
        {
            const string urlFormat = "v1/databases/{0}";

            var response = await httpClient.GetAsync(string.Format(urlFormat, databaseId));
            var result = await response.GetFromResponse<Database>(JsonOptions);

            return result;
        }

        public async Task<ObjectList<Page>> QueryDatabase(Guid databaseId, string[]? filterProperties = null, FilterObject? filter = null, SortObject[]? sorts = null)
        {
            const string urlFormat = "v1/databases/{0}/query/{1}";

            var queryParams = filterProperties == null
                ? ""
                : $"?filter_properties={string.Join("&filter_properties=", filterProperties)}";

            var response = await httpClient.PostAsJsonAsync(string.Format(urlFormat, databaseId, queryParams), new { filter, sorts }, JsonOptions);
            var result = await response.GetFromResponse<ObjectList<Page>>(JsonOptions);

            return result;
        }
    }
}
