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


        public async Task<Page> CreatePage(string name, string[] accounts, bool paid, int appId, bool installed)
        {
            const string url = "v1/pages";

            var parent = new ParentObject
            {
                type = ParentObjectType.database_id,
                database_id = config.DatabaseId
            };
            var properties = new Dictionary<string, PageProperty>
            {
                { "Available Accounts", new() { type = PropertyType.multi_select, multi_select = accounts.Select(x => new SelectOption { name = x }).ToArray() } },
                { "Paid", new() { type = PropertyType.checkbox, checkbox = paid } },
                { "appId", new() { type = PropertyType.number, number = appId } },
                { "Installed", new() { type = PropertyType.checkbox, checkbox = installed } },
                { "Name", new() { type = PropertyType.title, title = [ new RichTextObject { type = RichTextObjectType.Text, text = new Text { content = name } } ] } }
            };

            var response = await httpClient.PostAsJsonAsync(url, new { parent, properties }, JsonOptions);

            try
            {
                response.EnsureSuccessStatusCode();

                var model = await response.Content.ReadFromJsonAsync<Page>(JsonOptions)
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

        public async Task<Database> GetDatabase()
        {
            const string urlFormat = "v1/databases/{0}";

            var response = await httpClient.GetAsync(string.Format(urlFormat, config.DatabaseId));


            try
            {
                response.EnsureSuccessStatusCode();

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
