namespace Barathrum.GamesSync.Steam.Models.Response
{
    internal class SteamApiResponse<T>
    {
        public required T Response { get; init; }
    }
}
