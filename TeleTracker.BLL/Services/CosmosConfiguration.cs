using System;
using System.Collections.Generic;
using System.Text;
using TeleTracker.BLL.Interfaces;

namespace TeleTracker.BLL.Services
{
    public class CosmosConfiguration : ICosmosConfiguration
    {
        public CosmosConfiguration(string endpointUri, string primaryKey, string databaseId)
        {
            EndpointUri = endpointUri;
            PrimaryKey = primaryKey;
            DatabaseId = databaseId;
        }

        public string EndpointUri { get; set; }
        public string PrimaryKey { get; set; }
        public string DatabaseId { get; set; }
    }
}
