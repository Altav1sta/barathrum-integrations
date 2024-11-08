using Barathrum.GamesSync.Notion;
using Barathrum.GamesSync.Services;
using Barathrum.GamesSync.Steam;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, dude!");

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
        services.AddTransient<GameSyncingService>();

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

        var syncService = serviceProvider.GetRequiredService<GameSyncingService>();

        var accounts = await syncService.GetSteamAccounts();
        var accountsGames = await syncService.GetAccountsGames();

        await syncService.ClearNotionDatabase(notionConfig.GamesDbId);
        await syncService.ClearNotionDatabase(notionConfig.AccountsDbId);

        //await syncService.AddAccountsToNotion(accounts);
        //await syncService.AddGamesToNotion(accountsGames);


        Console.WriteLine("Success. Bye");
    }
}