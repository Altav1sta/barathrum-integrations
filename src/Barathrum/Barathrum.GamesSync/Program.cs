﻿using Barathrum.GamesSync.Notion;
using Barathrum.GamesSync.Steam;
using Barathrum.GamesSync.Steam.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;


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

var notionClient = serviceProvider.GetRequiredService<NotionClient>();
var steamClient = serviceProvider.GetRequiredService<SteamClient>();

var games = new Dictionary<int, Game>();
var accounts = new Dictionary<int, HashSet<string>>();

// get all games
foreach (var account in steamConfig.Accounts)
{
    var accountGames = await steamClient.GetPaidGamesList(account);
    
    foreach (var game in accountGames)
    {
        if (!games.ContainsKey(game.AppId))
        {
            games[game.AppId] = game;
        }

        if (accounts.TryGetValue(game.AppId, out HashSet<string>? value))
        {
            value.Add(account);
        }
        else
        {
            accounts[game.AppId] = [account];
        }
    }
}

// create all games
foreach (var game in games.Values)
{
    var page = await notionClient.CreatePage(game.Name!, [.. accounts[game.AppId]], true, game.AppId, false);
}

Console.WriteLine("Bye");