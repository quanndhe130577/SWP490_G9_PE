using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
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
            ResponseBuilder rpb = new ResponseBuilder().Error(context.Error.Message);
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
            ResponseBuilder rpb = new ResponseBuilder().Error(context.Error.Message);
            return rpb.ResponseModel;
            /*return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);*/
        }
    }
}
