using System.Net;

namespace TnR_SS.API.Common.Response
{
    public class ResponseModel<T> : ResponseModel
    {
        public T Data { get; set; }
        public ResponseModel(HttpStatusCode code, bool sc, string ms, T data) : base(code, sc, ms)
        {
            this.Data = data;
        }
        public ResponseModel() { }
    }
}
