using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeleTracker.Core.DTOs
{
    public class MovieDTO
    {
        [JsonProperty]
        public string ID { get; set; }

        [JsonProperty("original_title")]
        public string Title { get; set; }

        [JsonProperty("original_language")]
        public string Language { get; set; }
    }
}
