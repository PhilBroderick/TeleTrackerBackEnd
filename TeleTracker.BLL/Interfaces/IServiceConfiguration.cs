using System;
using System.Collections.Generic;
using System.Text;

namespace TeleTracker.BLL.Interfaces
{
    public interface IServiceConfiguration
    {
        string ApiKey { get; set; }
        string BaseUrl { get; set; }
    }
}
