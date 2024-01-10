using System.Reflection.PortableExecutable;
using System.Text;
using System;

namespace BD.CourseApp.Endpoint.Api.Middlewares
{
    public class BasicAuthenticationHandler
    {
        private readonly RequestDelegate next;
        private readonly string realm;

        public BasicAuthenticationHandler(RequestDelegate next, string realm)
        {
            this.next = next;
            this.realm = realm;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                await Unauthorized(context);
                return;
            }

            // Basic userid:password
            var header = context.Request.Headers["Authorization"].ToString();
            var encodedCreds = header.Substring(6);
            var creds = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCreds));
            string[] uidpwd = creds.Split(':');
            var uid = uidpwd[0];
            var password = uidpwd[1];

            if (uid != "bd" || password != "password")
            {
                await Unauthorized(context);
                return;
            }

            await next(context);
        }
        private async Task Unauthorized(HttpContext context)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
        }
    }
}
