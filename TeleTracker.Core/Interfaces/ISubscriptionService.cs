using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeleTracker.Core.DTOs;

namespace TeleTracker.Core.Interfaces
{
    public interface ISubscriptionService
    {
        Task InitializeService();
        Task<bool> SubscribeToShow(ShowSubscriptionDTO show, string userId);
        Task<IEnumerable<ShowSubscriptionDTO>> GetSubscribedShows(string userId);
    }
}
