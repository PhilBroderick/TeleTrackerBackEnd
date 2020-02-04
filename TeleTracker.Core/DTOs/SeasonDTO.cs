using System;
using System.Collections.Generic;
using System.Text;

namespace TeleTracker.Core.DTOs
{
    public class SeasonDTO
    {
        public int SeasonId { get; set; }
        public string Name { get; set; }
        public int EpisodeCount { get; set; }
        public IEnumerable<EpisodeDTO> EpisodeList { get; set; }
    }
}
