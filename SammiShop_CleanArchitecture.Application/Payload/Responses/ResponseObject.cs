using Microsoft.AspNetCore.Http;

namespace SammiShop_CleanArchitecture.Application.Payload.Responsi
{
    public class ResponseObject<T> where T : class
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public ResponseObject() { }

        public ResponseObject(int status, string message, T? data)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public ResponseObject<T> Success(string message, T data)
        => new ResponseObject<T>(StatusCodes.Status200OK, message, data);

        public ResponseObject<T> Error(int status, string message, T data)
        => new ResponseObject<T>(status, message, data);
    }
}
