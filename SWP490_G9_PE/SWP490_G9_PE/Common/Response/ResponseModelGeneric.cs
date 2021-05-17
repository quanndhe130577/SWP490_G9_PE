namespace SWP490_G9_PE.Common.Response
{
    public class ResponseModel<T> : ResponseModel
    {
        public T Data { get; set; }
        public ResponseModel(T data)
        {
            this.Data = data;
        }
        public ResponseModel(){}
    }
}
