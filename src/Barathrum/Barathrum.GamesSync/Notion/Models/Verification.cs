using Barathrum.GamesSync.Notion.Enums;

namespace Barathrum.GamesSync.Notion.Models
{
    internal class Verification
    {
        public required VerificationState state { get; init; }
        public User? verified_by { get; init; }
        public Date? date { get; init; }
    }
}
