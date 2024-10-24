namespace Barathrum.GamesSync.Steam.Models
{
    internal class SteamApiResponse<T>
    {
        public required T Response { get; init; }
    }
}
