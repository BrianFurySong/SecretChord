using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SecretChord.Filters
{
    public class AuthorizationRequiredAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Contains("Token"))
            {
                //TODO: check database and return true or false
                string tokenVal = actionContext.Request.Headers.GetValues("Token").First();
                string hardcodedToeknCheck = "shake_and_bake";
                if (tokenVal != hardcodedToeknCheck)
                {
                    actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
                    {
                        ReasonPhrase = "Tokens do not match!"
                    };
                }
            }
            else
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
                {
                    ReasonPhrase = "No token found."
                };
            }
            base.OnActionExecuting(actionContext);
        }
    }
}
