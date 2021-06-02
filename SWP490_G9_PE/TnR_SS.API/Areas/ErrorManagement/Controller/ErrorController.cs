using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TnR_SS.API.Common.ErrorHandle;
using TnR_SS.API.Common.Response;

namespace TnR_SS.API.Areas.ErrorManagement.Controller
{
    [Route("api")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public ResponseModel ErrorPublic([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName == "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            ResponseBuilder<object> rpb = new ResponseBuilder<object>().Error("Error").WithData(new ErrorResponse(new List<string>(new string[] { context.Error.Message, "Product Enviroment" })));
            return rpb.ResponseModel;
            /*return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);*/
        }

        [Route("error-local-development")]
        public ResponseModel ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            ResponseBuilder<object> rpb = new ResponseBuilder<object>().Error("Error").WithData(new ErrorResponse(new List<string>(new string[] { context.Error.Message, "Development Enviroment" })));
            return rpb.ResponseModel;
            /*return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);*/
        }
    }
}
