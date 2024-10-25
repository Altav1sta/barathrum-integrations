using Barathrum.GamesSync.Notion;
using Barathrum.GamesSync.Steam;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Text.Json;


Console.WriteLine("Hello, World!");

// ----------------- configure -----------------------

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddUserSecrets<Program>();

var config = builder.Build();
var services = new ServiceCollection();

var notionConfig = config.GetSection("Notion").Get<NotionConfig>()
    ?? throw new Exception("Notion config is null");
var steamConfig = config.GetSection("Steam").Get<SteamConfig>() 
    ?? throw new Exception("Steam config is null");

services.AddSingleton(notionConfig);
services.AddSingleton(steamConfig);

services.AddHttpClient<NotionClient>(client =>
{
    client.BaseAddress = new Uri(notionConfig.BaseAddress);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("Notion-Version", notionConfig.Version);
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", notionConfig.Secret);
});
services.AddHttpClient<SteamClient>(client =>
{
    client.BaseAddress = new Uri(steamConfig.BaseAddress);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var serviceProvider = services.BuildServiceProvider();


// --------------- run the app ------------------------

var client = serviceProvider.GetRequiredService<NotionClient>();
var db = await client.GetDatabase();
var resultView = JsonSerializer.Serialize(db, client.JsonOptions);
//var games = new HashSet<string>();

//foreach (var account in steamConfig.Accounts)
//{
//    var response = await client.GetPaidGamesList(account);
//    games.UnionWith(response.Games.Select(x => x.Name!));
//}

//var names = string.Join("\r\n", games);

Console.WriteLine(resultView);