using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;

namespace SecretChord.Filters
{
    public class ApiAuthenticationFilter : GenericAuthenticationFilter
    {
        public override bool OnAuthorizedUser(string userName, string password, HttpActionContext context)
        {
            //TODO: do database
            if (userName == "admin" && password == "Password1!")
            {
                BasicAuthenticationIdentity basicIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                if (basicIdentity != null)
                {
                    //these could come from ur database
                    basicIdentity.UserId = 1;
                    basicIdentity.Fullname = "admin";
                }
                return true;
            }
            //username and password did not match
            return false;
        }
    }
}
