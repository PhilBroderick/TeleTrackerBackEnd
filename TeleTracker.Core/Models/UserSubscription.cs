using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeleTracker.Core.Models
{
    public class UserSubscription
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        public List<string> ShowSubscriptionIds { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
