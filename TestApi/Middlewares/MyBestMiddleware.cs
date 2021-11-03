using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Middlewares
{
    public class MyBestMiddleware : IMiddleware
    {
        public MyBestMiddleware() { }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Query.ContainsKey("a"))
            {
                throw new Exception("hello world!!!");
            }
            else
                return next.Invoke(context);
        }
    }
}
