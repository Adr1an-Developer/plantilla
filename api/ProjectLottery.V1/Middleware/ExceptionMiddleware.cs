using ProjectLottery.V1.Domain.Data.Contexts;
using ProjectLottery.V1.Entities.Global;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Threading.Tasks;
using ProjectLottery.V1.Domain.DTOs.Common;
using ProjectLottery.V1.Helpers.Exceptions;
using Newtonsoft.Json.Linq;

namespace ProjectLottery.V1.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        static readonly Serilog.ILogger Log = Serilog.Log.ForContext<ExceptionMiddleware>();
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, DatabaseContext dbContext)
        {
            try
            {
                await _next(context);
            }
            catch (ExceptionCustom ex)
            {
                await HandleExceptionAsync(context, ex, dbContext);
            }
        }

        private static  Task HandleExceptionAsync(HttpContext context, ExceptionCustom exception, DatabaseContext dbContext)
        {
            var info = exception.Message.Split(",");

            Log.Error(exception.InnerException.Message, exception);

           var logEntry = new ExceptionLog
            {
                CreationDate = DateTime.Now,
                LogLevel = exception.InnerException.GetType().Name,
                Message = exception.InnerException.Message,
                StackTrace = exception.InnerException.StackTrace,
                IsActive = true,
                IsDeleted = false,
                UserId = info[0],
                ServiceName = info[1]
            };
            ResultExecption resultExecption = new ResultExecption();
            try
            {
                dbContext.exceptionLogs.Add(logEntry);
                dbContext.SaveChanges();
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                resultExecption.messages = new string[] { exception.InnerException.Message };

                return context
                       .Response
                       .WriteAsync(JsonConvert
                                   .SerializeObject(resultExecption,
                                       new JsonSerializerSettings
                                       {
                                           ContractResolver = new CamelCasePropertyNamesContractResolver()
                                       }));
            }
            catch (Exception e )
            {
                resultExecption.messages = new string[] { e.InnerException.Message };
                return context
                     .Response
                     .WriteAsync(JsonConvert
                                 .SerializeObject(resultExecption,
                                     new JsonSerializerSettings
                                     {
                                         ContractResolver = new CamelCasePropertyNamesContractResolver()
                                     }));
            }
            
        }
    }
}
