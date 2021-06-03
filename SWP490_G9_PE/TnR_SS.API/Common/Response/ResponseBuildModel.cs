using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TnR_SS.API.Common.Response
{
    public class ResponseBuilder
    {
        public ResponseModel ResponseModel { get; set; }
        public ResponseBuilder()
        {
            this.ResponseModel = new ResponseModel();
        }


        public ResponseBuilder Success(string message = null)
        {
            this.ResponseModel.Success = true;
            this.ResponseModel.Message = message;
            this.ResponseModel.StatusCode = HttpStatusCode.OK;

            return this;
        }

        public ResponseBuilder Error(string message = null)
        {
            this.ResponseModel.Success = false;
            this.ResponseModel.Message = message;
            this.ResponseModel.StatusCode = HttpStatusCode.BadRequest;

            return this;
        }

        public ResponseBuilder Errors(List<string> rs)
        {
            this.ResponseModel = new ResponseModel<object>();
            this.ResponseModel.Success = false;
            this.ResponseModel.StatusCode = HttpStatusCode.BadRequest;
            this.ResponseModel.Message = "Errors";
            ((ResponseModel<object>)this.ResponseModel).Data = new { errors = rs };
            return this;
        }

        public ResponseBuilder WithCode(HttpStatusCode statusCode)
        {
            this.ResponseModel.StatusCode = statusCode;
            return this;
        }

        public ResponseBuilder WithMessage(string ms)
        {
            this.ResponseModel.Message = ms;
            return this;
        }

        public ResponseModel Build()
        {
            return this.ResponseModel;
        }

        public static ResponseBuilder<T> NewBuilder<T>()
        {
            return new ResponseBuilder<T>();
        }

        public static ResponseBuilder NewBuilder()
        {
            return new ResponseBuilder();
        }
    }

    public class ResponseBuilder<T> : ResponseBuilder
    {
        public ResponseBuilder()
        {
            this.ResponseModel = new ResponseModel<T>();
        }

        public new ResponseBuilder<T> Success(string message = null)
        {
            base.Success(message);
            return this;
        }

        public new ResponseBuilder<T> Error(string message = null)
        {
            base.Error(message);
            return this;
        }

        public new ResponseBuilder<T> WithCode(HttpStatusCode statusCode)
        {
            base.WithCode(statusCode);
            return this;
        }

        public ResponseBuilder<T> WithData(T data)
        {
            //base.Success("Success");
            ((ResponseModel<T>)this.ResponseModel).Data = data;
            return this;
        }

        public new ResponseModel<T> Build()
        {
            return (ResponseModel<T>)this.ResponseModel;
        }
    }

}
