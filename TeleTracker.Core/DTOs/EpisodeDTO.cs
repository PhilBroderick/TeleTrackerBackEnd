using System;

namespace TeleTracker.Core.DTOs
{
    public class EpisodeDTO
    {
        public int EpisodeId { get; set; }
        public string Name { get; set; }
        public TimeSpan Runtime { get; set; }
        public float Rating { get; set; }
    }
}