using Barathrum.GamesSync.Notion.Enums;

namespace Barathrum.GamesSync.Notion.Models
{
    internal class Annotation
    {
        public bool Bold { get; init; }

        public bool Italic { get; init; }

        public bool Strikethrough { get; init; }

        public bool Underline { get; init; }

        public bool Code { get; init; }

        public Color Color { get; init; }
    }
}
