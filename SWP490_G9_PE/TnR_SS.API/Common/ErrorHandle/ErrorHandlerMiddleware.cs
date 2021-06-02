using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;

namespace TnR_SS.API.Common.ErrorHandle
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
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                /*switch (error)
                {
                    case Exception e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }*/

                ResponseBuilder<object> rpb = new ResponseBuilder<object>().Error("Error").WithData(new ErrorResponse(new List<string>(new string[] { error?.Message, "Error Handler Middleware" })));
                //var result = JsonSerializer.Serialize(rpb.ResponseModel);
                var result = JsonConvert.SerializeObject(rpb.ResponseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
