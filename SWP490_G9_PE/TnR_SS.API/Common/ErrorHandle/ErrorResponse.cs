using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TnR_SS.API.Common.ErrorHandle
{
    public class ErrorResponse
    {
        public List<string> Errors { get; set; }

        public ErrorResponse(List<string> ers)
        {
            this.Errors = ers;
        }
        public ErrorResponse() { }
    }
}
