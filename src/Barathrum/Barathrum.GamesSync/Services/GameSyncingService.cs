using Barathrum.GamesSync.Notion;
using Barathrum.GamesSync.Notion.Enums;
using Barathrum.GamesSync.Notion.Models.Blocks;
using Barathrum.GamesSync.Notion.Models;
using Barathrum.GamesSync.Steam;
using Barathrum.GamesSync.Steam.Models;

namespace Barathrum.GamesSync.Services
{
    internal class GameSyncingService(SteamClient steamClient, SteamConfig steamConfig, NotionClient notionClient, NotionConfig notionConfig)
    {
        private readonly SteamClient steamClient = steamClient;
        private readonly SteamConfig steamConfig = steamConfig;
        private readonly NotionClient notionClient = notionClient;
        private readonly NotionConfig notionConfig = notionConfig;

        public async Task<Player[]> GetSteamAccounts()
        {
            Console.WriteLine("Getting accounts info from Steam...");

            var accounts = await steamClient.GetPlayerSummaries(steamConfig.Accounts.All);

            return accounts;
        }

        public async Task<Dictionary<string, Game[]>> GetAccountsGames()
        {
            Console.WriteLine("Getting available games from Steam accounts...");

            var accounts = steamConfig.Accounts.All;
            var accountsGames = new Dictionary<string, Game[]>(accounts.Length);

            foreach (var account in accounts)
            {
                var allGames = await steamClient.GetGamesList(account, true);
                var paidGames = await steamClient.GetGamesList(account, false);
                var paidGamesIds = paidGames.Select(x => x.AppId).ToHashSet();

                foreach (var game in allGames)
                {
                    if (!paidGamesIds.Contains(game.AppId))
                    {
                        game.IsFreeToPlay = true;
                    }
                }

                accountsGames[account] = allGames;
            }

            return accountsGames;
        }

        public async Task AddAccountsToNotion(Player[] players)
        {
            Console.WriteLine("Adding accounts to Notion database...");

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

                var page = await notionClient.CreatePage(notionConfig.AccountsDbId, properties);
                var children = await notionClient.AppendBlockChildren(page.id!, [image]);
            }
        }

        public async Task AddGamesToNotion(Dictionary<string, Game[]> accountsGames)
        {
            var gamesByIds = GetGamesByIds(accountsGames);
            var gamesAccounts = GetAvailableAccountsForGames(accountsGames);
            var accountsPageIds = await GetNotionPageIdsForAccounts();

            Console.WriteLine("Adding games to Notion database...");

            foreach (var gameAccounts in gamesAccounts)
            {
                var properties = new Dictionary<string, PageProperty>
                {
                    { "Shared Accounts", new() { type = PropertyType.relation, relation = gameAccounts.Value.Select(x => new Page { id = accountsPageIds[x] }).ToArray() } },
                    { "Paid", new() { type = PropertyType.checkbox, checkbox = !gamesByIds[gameAccounts.Key].IsFreeToPlay } },
                    { "appId", new() { type = PropertyType.number, number = gameAccounts.Key } },
                    { "Installed", new() { type = PropertyType.checkbox, checkbox = false } },
                    { "Name", new() { type = PropertyType.title, title = [ new RichTextObject { type = RichTextObjectType.Text, text = new Text { content = gamesByIds[gameAccounts.Key].Name! } } ] } }
                };
                var page = await notionClient.CreatePage(notionConfig.GamesDbId, properties);
            }
        }


        private static Dictionary<int, Game> GetGamesByIds(Dictionary<string, Game[]> accountsGames)
        {
            var result = accountsGames
                .SelectMany(x => x.Value)
                .DistinctBy(x => x.AppId)
                .ToDictionary(x => x.AppId);

            return result;
        }

        private Dictionary<int, List<string>> GetAvailableAccountsForGames(Dictionary<string, Game[]> accountsGames)
        {
            var gamesAccounts = new Dictionary<int, List<string>>();

            foreach (var accountGames in accountsGames)
            {
                var isPersonalAccount = steamConfig.Accounts.Personal.Contains(accountGames.Key);

                foreach (var game in accountGames.Value)
                {
                    if (gamesAccounts.TryGetValue(game.AppId, out var value))
                    {
                        if (!isPersonalAccount) value.Add(accountGames.Key);
                    }
                    else
                    {
                        gamesAccounts[game.AppId] = isPersonalAccount ? [] : [accountGames.Key];
                    }
                }
            }

            return gamesAccounts;
        }

        private async Task<Dictionary<string, string>> GetNotionPageIdsForAccounts()
        {
            Console.WriteLine("Getting \"Game Accounts\" database info from Notion...");

            var db = await notionClient.GetDatabase(notionConfig.AccountsDbId);

            Console.WriteLine("Querying accounts pages from Notion...");

            var filterProperties = new string[] { db.properties["AccountId"].id! };
            var pages = await notionClient.QueryDatabase(notionConfig.AccountsDbId, filterProperties);
            var result = pages.results!.ToDictionary(x => x.properties!["AccountId"]!.rich_text![0]!.text!.content, x => x.id!);

            return result;
        }
    }
}
