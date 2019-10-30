using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeleTracker.CustomResponses
{
    public class SuccessfulSubscribeResponse : ICustomResponse
    {
        public string UserID { get ; }
        public string EntityID { get;}
        public string Message
        {
            get
            {
                return $"User {UserID} successfully subscribed to {EntityID}";
            }
        }
        public SuccessfulSubscribeResponse(string userID, string showID)
        {
            UserID = userID;
            EntityID = showID;
        }
    }
}
