using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CrudDenemeleri.MiddleWares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Beklenmeyen bir hata oluştu...");
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application.json";
            context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;

            var result = Newtonsoft.Json.JsonConvert.SerializeObject(new { error = "Beklenmeyen bir hata oluştu.." , details = exception.Message});

            return context.Response.WriteAsync(result);
        }
    }
}