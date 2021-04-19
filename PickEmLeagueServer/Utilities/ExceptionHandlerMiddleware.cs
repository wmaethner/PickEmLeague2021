using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace PickEmLeagueAPI.Utilities
{
    public class ExceptionHandlerMiddleware
    {
        private readonly bool _returnException;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this.next = next;
            _returnException = configuration.GetValue("ReturnExceptions", true);
        }

        public async Task Invoke(HttpContext context, ILogger<ExceptionHandlerMiddleware> logger)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, logger, _returnException);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception,
            ILogger<ExceptionHandlerMiddleware> logger, bool returnException)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            // For AggregateException types, grab the first exception.
            if (exception is AggregateException aggregateException)
            {
                exception = aggregateException.Flatten().InnerException;
            }

            
            
            var traceId = Activity.Current?.Id ?? context?.TraceIdentifier;
            context!.Response.Headers.Add("TraceId", traceId);
            var result = JsonConvert.SerializeObject(new Exception(exception.Message + " --- Through Middleware"));
            logger.LogError(
                $"Api exception {traceId}: {exception.Message} StatusCode: {statusCode}");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
