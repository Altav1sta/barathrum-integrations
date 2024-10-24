namespace Barathrum.GamesSync.Notion.Models
{
    internal class Status
    {
        public required SelectOption[] options { get; init; }
        public required SelectOptionsGroup[] groups { get; init; }
    }
}
