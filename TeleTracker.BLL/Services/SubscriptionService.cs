using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeleTracker.Core.Interfaces;

namespace TeleTracker.BLL.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        public async Task<bool> SubscribeToShow(string showId, string userId)
        {
            return true;
        }
    }
}
