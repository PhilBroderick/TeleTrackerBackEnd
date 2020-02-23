using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeleTracker.CustomResponses
{
    public class SubscribeToShowResponse : ICustomResponse
    {
        private bool _isSuccess;
        public string UserID { get ; }
        public string EntityID { get;}
        public string Message
        {
            get
            {
                return string.Concat(UserID, _isSuccess ? " successfully" : " unsuccessfully", " subscribed to ", EntityID);
            }
        }

        public SubscribeToShowResponse(string userID, string showID, bool isSuccess)
        {
            UserID = userID;
            EntityID = showID;
            _isSuccess = isSuccess;
        }
    }
}
