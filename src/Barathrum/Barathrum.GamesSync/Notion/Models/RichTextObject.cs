using Barathrum.GamesSync.Notion.Enums;

namespace Barathrum.GamesSync.Notion.Models
{
    internal class RichTextObject
    {
        public required RichTextObjectType type { get; init; }
        public Text? text { get; init; }
        public Mention? mention { get; init; }
        public Equation? equation { get; init; }
        public required Annotation annotations { get; init; }
        public required string plain_text { get; init; }
        public string? href { get; init; }
    }
}
