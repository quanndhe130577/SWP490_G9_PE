using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TnR_SS.API.Common.Response
{
    public class ResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public string Type { get; set; }

        public ResponseModel() { }
        public ResponseModel(HttpStatusCode code, bool sc, string ms, string type)
        {
            this.StatusCode = code;
            this.Success = sc;
            this.Message = ms;
            this.Type = type;
        }

    }
}
