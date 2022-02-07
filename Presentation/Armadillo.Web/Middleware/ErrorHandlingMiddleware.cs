using Armadillo.Core.Exception;
using Armadillo.Core.Serializer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Armadillo.Web.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly IJsonSerializer jsonSerializer;
        private readonly ILogger<ErrorHandlingMiddleware> logger;
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(
            IJsonSerializer jsonSerializer,
            ILogger<ErrorHandlingMiddleware> logger,
            RequestDelegate next)
        {
            this.jsonSerializer = jsonSerializer;
            this.logger = logger;
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"middleware exception message: {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            string message = "Unexpected error occurred!";

            if (exception is BusinessException)
            {
                message = exception.Message;
            }

            return context.Response.WriteAsync(jsonSerializer.Serialize(new
            {
                IsSuccess = false,
                Message = message
            }));
        }
    }
}
