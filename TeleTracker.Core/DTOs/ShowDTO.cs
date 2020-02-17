using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeleTracker.Core.DTOs
{
    public class ShowDTO
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public DateTime FirstAirDate { get; set; }
        public string Overview { get; set; }
        public bool InProduction { get; set; }
        public IEnumerable<SeasonDTO> Seasons { get; set; }
        public string PosterPath { get; set; }
    }
}
