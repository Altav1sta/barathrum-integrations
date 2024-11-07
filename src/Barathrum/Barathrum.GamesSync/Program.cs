using Barathrum.GamesSync.Notion;
using Barathrum.GamesSync.Notion.Enums;
using Barathrum.GamesSync.Notion.Models;
using Barathrum.GamesSync.Notion.Models.Blocks;
using Barathrum.GamesSync.Steam;
using Barathrum.GamesSync.Steam.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

internal class Program
{
    private static async Task Main(string[] args)
    {
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

        var accounts = await GetAccountsInfo(steamClient, steamConfig);
        var accountsGames = await GetAvailableGames(steamClient, steamConfig);
        var sharedAccounts = GetAvailableAccountsForGames(accountsGames, steamConfig.PersonalAccount);

        await AddAccountsToNotion(notionClient, notionConfig, [.. accounts.Values]);

        // create all games
        //foreach (var game in games.Values)
        //{
        //    var properties = new Dictionary<string, PageProperty>
        //    {
        //        { "Available Accounts", new() { type = PropertyType.multi_select, multi_select = sharedAccounts[game.AppId].Select(x => new SelectOption { name = x }).ToArray() } },
        //        { "Paid", new() { type = PropertyType.checkbox, checkbox = true } },
        //        { "appId", new() { type = PropertyType.number, number = game.AppId } },
        //        { "Installed", new() { type = PropertyType.checkbox, checkbox = false } },
        //        { "Name", new() { type = PropertyType.title, title = [ new RichTextObject { type = RichTextObjectType.Text, text = new Text { content = game.Name! } } ] } }
        //    };
        //    var page = await notionClient.CreatePage(properties);
        //}

        Console.WriteLine("Bye");
    }


    private static async Task<Dictionary<string, Player>> GetAccountsInfo(SteamClient steamClient, SteamConfig steamConfig)
    {
        var steamIds = new List<string>(steamConfig.Accounts)
        {
            steamConfig.PersonalAccount
        };

        var accounts = await steamClient.GetPlayerSummaries(steamIds);
        var result = accounts.ToDictionary(x => x.SteamId);

        return result;
    }

    private static async Task<Dictionary<string, Game[]>> GetAvailableGames(SteamClient steamClient, SteamConfig steamConfig)
    {
        var games = new Dictionary<string, Game[]>
        {
            [steamConfig.PersonalAccount] = await steamClient.GetGamesList(steamConfig.PersonalAccount, true)
        };

        foreach (var account in steamConfig.Accounts)
        {
            games[steamConfig.PersonalAccount] = await steamClient.GetGamesList(account, false);
        }

        return games;
    }

    private static Dictionary<int, HashSet<string>> GetAvailableAccountsForGames(Dictionary<string, Game[]> accountsGames, string personalAccount)
    {
        var gamesAccounts = new Dictionary<int, HashSet<string>>();

        foreach (var accountGames in accountsGames)
        {
            if (accountGames.Key == personalAccount)
            {
                continue;
            }

            foreach (var game in accountGames.Value)
            {
                if (gamesAccounts.TryGetValue(game.AppId, out HashSet<string>? value))
                {
                    value.Add(accountGames.Key);
                }
                else
                {
                    gamesAccounts[game.AppId] = [accountGames.Key];
                }
            }
        }

        return gamesAccounts;
    }

    private static async Task AddAccountsToNotion(NotionClient notionClient, NotionConfig config, Player[] players)
    {
        foreach (var player in players)
        {
            var properties = new Dictionary<string, PageProperty>
            {
                { "Name", new() { type = PropertyType.title, title = [ new RichTextObject { type = RichTextObjectType.Text, text = new Text { content = player.Name } } ] } },
                { "AccountId", new() { type = PropertyType.rich_text, rich_text = [ new RichTextObject { type = RichTextObjectType.Text, text = new Text { content = player.SteamId } } ] } },
                { "Platform", new() { type = PropertyType.select, select = new SelectOption { name = "Steam" } } },
            };
            var image = new ImageBlock 
            {
                type = "image",
                image = new FileObject
                {
                    type = FileObjectType.external,
                    external = new ExternalFile { url = player.AvatarFullUrl }
                }
            };

            var page = await notionClient.CreatePage(config.AccountsDbId, properties);
            var children = await notionClient.AppendBlockChildren<ImageBlock>(page.id!, [image]);
        }
    }
}