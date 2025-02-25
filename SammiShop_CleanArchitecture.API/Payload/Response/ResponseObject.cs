namespace SammiShop_CleanArchitecture.Application.Payload.Response
{
    public class ResponseObject<T>
    {

        public int Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ResponseObject() { }

        public ResponseObject(int status, string message, T? data)
        {
            Status = status;
            Message = message;
            Data = data;
        }


        public ResponseObject<T> Success(string message, T data)
        {
            return new ResponseObject<T>
            {
                Status = StatusCodes.Status200OK,
                Message = message,
                Data = data
            };
        }
        public ResponseObject<T> Error(int status, string message, T data)
        {
            return new ResponseObject<T>
            {
                Status = status,
                Message = message,
                Data = data
            };
        }


    }
}
