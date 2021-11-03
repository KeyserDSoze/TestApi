using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Middlewares
{
    public class FastAuthMiddleware : IMiddleware
    {
        public FastAuthMiddleware() { }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Headers.ContainsKey("my-super-auth"))
            {
                if (context.Request.Headers["my-super-auth"] == "dasjdas knclalndfnjf jns fdnakjfcjasnfc sanjlsnad as")
                {
                    return next.Invoke(context);
                }
            }
            context.Response.StatusCode = 800;
            return Task.CompletedTask;
        }
    }
}
