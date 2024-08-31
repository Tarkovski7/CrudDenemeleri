using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CrudDenemeleri.MiddleWares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger
        )
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex) when (ex is ValidationException || ex is ArgumentException)
            {
                _logger.LogWarning(ex, "Uyari : İsleme alinamayan bir istek.");
                await HandleExceptionAsync(context, ex);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Beklenmeyen bir hata olustu...");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "text/html";
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var redirectUrl = "/Error";

            switch (exception)
            {
                case UnauthorizedAccessException _:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    redirectUrl = "/Error/UnAuthorized";
                    break;
                case ArgumentException _:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    redirectUrl  = exception is ValidationException ? "/Error/ValidationError" : "/Error/BadRequest";
                    break;
                case NullReferenceException _:
                    statusCode = (int)HttpStatusCode.NotFound;
                    redirectUrl = "/Error/NotFound";
                    break;
                default:
                    redirectUrl = "/Error";
                    break;
            }
            context.Response.StatusCode = statusCode;

            // var result = Newtonsoft.Json.JsonConvert.SerializeObject(
            //     new { error = errorMessage, details = exception.Message }
            // );
            context.Response.Redirect(redirectUrl);
            return Task.CompletedTask;
        }
    }
}
