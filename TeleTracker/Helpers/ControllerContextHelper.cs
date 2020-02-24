using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TeleTracker.Helpers
{
    public static class ControllerContextHelper
    {
        public static string GetCurrentUserID(HttpContext currentContext)
        {
            return currentContext.User.Identity is ClaimsIdentity identity ?
                identity.FindFirst(ClaimTypes.NameIdentifier).Value : string.Empty;
        }
    }
}
