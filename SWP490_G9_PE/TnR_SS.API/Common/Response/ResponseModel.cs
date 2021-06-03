using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TnR_SS.API.Common.Response
{
    public class ResponseModel
    {
        [Required]
        public HttpStatusCode StatusCode { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public ResponseModel() { }
        public ResponseModel(HttpStatusCode code, bool sc, string ms)
        {
            this.StatusCode = code;
            this.Success = sc;
            this.Message = ms;
        }

    }
}
