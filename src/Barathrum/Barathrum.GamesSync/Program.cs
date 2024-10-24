using Barathrum.GamesSync.Steam;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


Console.WriteLine("Hello, World!");

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddUserSecrets<Program>();

var config = builder.Build();
var services = new ServiceCollection();

var steamConfig = config.GetSection("Steam").Get<SteamConfig>() 
    ?? throw new Exception("Steam config is null");

services.AddSingleton(steamConfig);
services.AddHttpClient<SteamClient>(client =>
{
    client.BaseAddress = new Uri(steamConfig.BaseAddress);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var serviceProvider = services.BuildServiceProvider();


var client = serviceProvider.GetRequiredService<SteamClient>();
var games = new HashSet<string>();

foreach (var account in steamConfig.Accounts)
{
    var response = await client.GetPaidGamesList(account);
    games.UnionWith(response.Games.Select(x => x.Name!));
}

var names = string.Join("\n\r", games);

Console.WriteLine(names);