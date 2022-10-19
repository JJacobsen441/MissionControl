using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MissionControl.Filters
{
    /*
     * not my code, but heavyly modified
     * it took awhile to figure out I was in the wrong namespace
     * */
    public class BasicAuthenticationFilter : ActionFilterAttribute
    {
        public string BasicRealm { get; set; }
        protected string Username { get; set; }
        protected string Password { get; set; }

        public BasicAuthenticationFilter(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public override void OnActionExecuting(HttpActionContext context)
        {
            HttpRequestMessage req = context.Request;
            string auth = req.Headers.Authorization.ToString();
            if (!string.IsNullOrEmpty(auth) && /*dont know if this is the way*/auth.StartsWith("Basic"))
            {
                string[] cred = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
                var user = new { Name = cred[0], Pass = cred[1] };
                if (user.Name == Username && user.Pass == Password) 
                    return;
            }

            //context.Response = context.Request.CreateResponse(HttpStatusCode.Unauthorized);
            
            context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);

            //context.Response.Headers.Add("WWW-Authenticate", String.Format("Basic realm=\"{0}\"", BasicRealm ?? "mydomain"));
        }
    }    
}
