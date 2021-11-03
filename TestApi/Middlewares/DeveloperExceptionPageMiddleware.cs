using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Middlewares
{
    public class DeveloperExceptionPageMiddleware : IMiddleware
    {
        public DeveloperExceptionPageMiddleware() { }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                //prima della catena quando tocca a me
                string ciao = string.Empty;
                await next.Invoke(context).ConfigureAwait(false);
                string hola = string.Empty;
                //scrivo alla fine della catena nel verso opposto

            }
            catch (Exception ex)
            {
                await context.Response.WriteAsync(ex.ToString()).ConfigureAwait(false);
            }
        }
    }
}
