using System;
using System.Collections.Generic;
using System.Text;

namespace TeleTracker.Core.DTOs
{
    public class ShowPopularityDTO
    {
        public DateTime? FirstAirDate { get; set; }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public List<string> OriginCountry { get; set; }
        public string BackdropPath { get; set; }
        public List<int> GenreIds { get; set; }
        public string OriginalLanguage { get; set; }
        public string Overview { get; set; }
        public string PosterPath { get; set; }
        public double VoteAverage { get; set; }
        public int VoteCount { get; set; }
        public string ID { get; set; }
        public double Popularity { get; set; }
    }
}
