using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HelloWorldPlugin.Middlewares
{
    public class SayHelloMiddleware
    {
        private readonly RequestDelegate _next;

        public SayHelloMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            bool isMatch = false;

            isMatch = httpContext.Request.Path.Value.StartsWith("/SayHello");

            if (isMatch)
            {
                await httpContext.Response.WriteAsync($"Hello World! {DateTime.Now:yyyy-MM-dd HH:mm:ss}", Encoding.UTF8);
            }
            else
            {
                // Call the next delegate/middleware in the pipeline
                await _next(httpContext);
            }
        }

    }
}
