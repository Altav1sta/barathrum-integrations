namespace Barathrum.GamesSync.Steam
{
    internal class SteamConfig
    {
        public required string ApiKey { get; init; }
        public required string BaseAddress { get; init; }
        public required AccountsConfig Accounts { get; init; }

        public class AccountsConfig
        {
            public required string[] Personal { get; init; }
            public required string[] Shared { get; init; }
            public string[] All => Personal.Concat(Shared).Distinct().ToArray();
        }
    }
}
