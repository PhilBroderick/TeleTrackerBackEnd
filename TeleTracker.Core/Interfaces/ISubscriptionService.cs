using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TeleTracker.Core.Interfaces
{
    public interface ISubscriptionService
    {
        Task InitializeService();
        Task<bool> SubscribeToShow(string showId, string userId);
        Task<IEnumerable<string>> GetSubscribedShows(string userId);
    }
}
