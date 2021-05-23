using System.Net;

namespace TnR_SS.API.Common.Response
{
    public class ResponseModel<T> : ResponseModel
    {
        public T Data { get; set; }
        public ResponseModel(HttpStatusCode code, bool sc, string ms, string type, T data) : base(code, sc, ms, type)
        {
            this.Data = data;
        }
        public ResponseModel() { }
    }
}
