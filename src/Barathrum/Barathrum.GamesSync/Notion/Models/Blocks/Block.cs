namespace Barathrum.GamesSync.Notion.Models.Blocks
{
    internal class Block
    {
        public required string @object { get; init; }
        public required string id { get; init; }
        public ParentObject? parent { get; init; }
        public string? type { get; init; }
    }
}
