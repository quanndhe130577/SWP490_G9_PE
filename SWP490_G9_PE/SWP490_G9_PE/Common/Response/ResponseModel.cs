using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SWP490_G9_PE.Common.Response
{
    public class ResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public string Type { get; set; }

        public override string ToString()
        {
            return $"[{Success.ToString().ToUpper()} : {StatusCode}] {Message}";
        }

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
