﻿namespace Barathrum.GamesSync.Notion
{
    internal class NotionConfig
    {
        public required string Secret { get; init; }
        public required string BaseAddress { get; init; }
        public required string Version { get; init; }
        public required Guid AccountsDbId { get; init; }
        public required Guid GamesDbId { get; init; }
    }
}
