using System;
using System.Collections.Generic;
using System.Text;

namespace TeleTracker.BLL.Interfaces
{
    public interface ICosmosConfiguration
    {
        string EndpointUri { get; set; }
        string PrimaryKey { get; set; }
        string DatabaseId { get; set; }
    }
}
