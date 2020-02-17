using System;
using System.Collections.Generic;
using System.Text;
using TeleTracker.BLL.Interfaces;

namespace TeleTracker.BLL.Services
{
    public class ServiceConfiguration : IServiceConfiguration
    {
        public ServiceConfiguration(string apiKey)
        {
            ApiKey = apiKey;
        }
        public string ApiKey { get; set; }
    }
}
