using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace WebAPI.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next; // Middleware tiếp theo trong pipeline

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"Có request: {context.Request.Method} {context.Request.Path}");

            // Gọi middleware tiếp theo
            await _next(context);

            Console.WriteLine($"Trả về mã: {context.Response.StatusCode}");
        }
    }
}
