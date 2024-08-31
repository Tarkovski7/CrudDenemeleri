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
                _logger.LogWarning(ex, "Uyarı : İşleme alınamayan bir istek.");
                await HandleExceptionAsync(context, ex);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Beklenmeyen bir hata oluştu...");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application.json";
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var errorMessage = "Beklenmeyen bir hata oluştu.";
            switch (exception)
            {
                case UnauthorizedAccessException _:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    errorMessage = "Yetkisiz erişim.";
                    break;
                case ArgumentException _:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    errorMessage = "Geçersiz parametre.";
                    break;
                case ValidationException _:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    errorMessage = "Doğrulama Hatası.";
                    break;
                default:
                    errorMessage = exception.Message;
                    break;
            }
            context.Response.StatusCode = statusCode;

            var result = Newtonsoft.Json.JsonConvert.SerializeObject(
                new { error = errorMessage, details = exception.Message }
            );

            return context.Response.WriteAsync(result);
        }
    }
}
