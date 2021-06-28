using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LokalnyTarg.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {

                await HandleErrorAsync(context, exception);
            }
        }

        private Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var exceptionResult = GetException(exception);
            var response = new { message = exceptionResult.Item1 };
            var payload = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exceptionResult.Item2;

            return context.Response.WriteAsync(payload);
        }

        private (string, int) GetException(Exception exception)
        {
            var key = _exceptionDictionary.Keys.FirstOrDefault(x => x(exception));
            if (key == null)
            {
                return ("Internal Server Error", 500);
            }
            return _exceptionDictionary[key](exception);
        }

        private readonly Dictionary<Func<Exception, bool>, Func<Exception, (string, int)>> _exceptionDictionary =
            new Dictionary<Func<Exception, bool>, Func<Exception, (string, int)>>
            {

            };


    }
}
