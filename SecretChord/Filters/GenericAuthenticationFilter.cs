using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SecretChord.Filters
{
    public class GenericAuthenticationFilter : AuthorizationFilterAttribute
    {
        public virtual BasicAuthenticationIdentity FetchHeader(HttpActionContext context)
        {
            string authHeaderVal = null;
            System.Net.Http.Headers.AuthenticationHeaderValue authRequest = context.Request.Headers.Authorization;
            if (authRequest != null && !string.IsNullOrEmpty(authRequest.Scheme) && authRequest.Scheme == "Basic")
            {
                authHeaderVal = authRequest.Parameter;
            }
            if (string.IsNullOrEmpty(authHeaderVal)) return null;
            authHeaderVal = Encoding.Default.GetString(Convert.FromBase64String(authHeaderVal));
            //pattern for header should be username:password
            string[] creds = authHeaderVal.Split(':');
            return creds.Length < 2 ? null : new BasicAuthenticationIdentity(creds[0], creds[1]);
        }

        public virtual bool OnAuthorizedUser(string userName, string password, HttpActionContext context)
        {
            //TODO: maybe add some better validation too?
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                //TODO: check your database using username and password here
                return false;
            }
            return true;
        }

        private static void ChallengeAuthRequest(HttpActionContext context)
        {
            string dnsHost = context.Request.RequestUri.DnsSafeHost;
            context.Response = context.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
            context.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", dnsHost));
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            BasicAuthenticationIdentity identity = FetchHeader(actionContext);
            if (identity == null)
            {
                ChallengeAuthRequest(actionContext);
                return;
            }
            //TODO: if you have roles add them here
            GenericPrincipal gp = new GenericPrincipal(identity, null);
            Thread.CurrentPrincipal = gp;
            if (!OnAuthorizedUser(identity.UserName, identity.Password, actionContext))
            {
                ChallengeAuthRequest(actionContext);
                return;
            }
            base.OnAuthorization(actionContext);
        }
    }

    public class BasicAuthenticationIdentity : GenericIdentity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public string Fullname { get; set; }

        public BasicAuthenticationIdentity(string userName, string password) : base(userName, "Basic")
        {
            UserName = userName;
            Password = password;
        }
    }
}
