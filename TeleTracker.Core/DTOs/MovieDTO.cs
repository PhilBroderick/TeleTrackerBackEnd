using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeleTracker.Core.DTOs
{
    public class MovieDTO
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? Runtime { get; set; }
        public string PosterPath { get; set; }
        public string Overview { get; set; }
        public double Popularity { get; set; }
        public int VoteCount { get; set; }
    }
}
