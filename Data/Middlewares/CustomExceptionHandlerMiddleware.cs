using System.Net;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using MyPastebin.Data.Exceptions;
using System.Security.Authentication;

namespace MyPastebin.Data.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next) =>
            _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch(exception)
            {
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                case AuthenticationException:
                    code = HttpStatusCode.Unauthorized;
                    // result = JsonSerializer.Serialize(exception.Message);
                    break;
                case RegisterException:
                    code = HttpStatusCode.Conflict;
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { errpr = exception.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }
}