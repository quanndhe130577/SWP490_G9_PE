using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;

namespace TnR_SS.API.Middleware.ErrorHandle
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
                if (!String.IsNullOrEmpty(context.Request.Query["id"]))// parameter
                {
                    int id = int.Parse(context.Request.Query["id"]);

                    if (!TokenManagement.CheckUserIdFromToken(context, id))
                    {
                        throw new Exception("Access denied");
                    }
                }
                    
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
                ResponseBuilder rpb = new ResponseBuilder().Error(error?.InnerException == null ? error?.Message : error?.InnerException.Message);
                //var result = JsonSerializer.Serialize(rpb.ResponseModel);
                var result = JsonConvert.SerializeObject(rpb.ResponseModel);
                await response.WriteAsync(result);
            }
        }

        private Exception Exception(string v)
        {
            throw new NotImplementedException();
        }
    }
}
