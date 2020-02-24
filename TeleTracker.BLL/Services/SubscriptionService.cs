using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeleTracker.Core.Interfaces;

namespace TeleTracker.BLL.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        public Task<IEnumerable<string>> GetSubscribedShows(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SubscribeToShow(string showId, string userId)
        {
            return true;
        }
    }
}
