namespace Barathrum.GamesSync.Steam
{
    internal class SteamConfig
    {
        public required string ApiKey { get; init; }
        public required string BaseAddress { get; init; }
        public required string[] Accounts { get; init; }
    }
}
