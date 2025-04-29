using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RoboostTask.GeneralResponse
{
    public class Response<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool IsSucceeded { get; set; }
        public Response() { }

        public Response<T> Success(T data, string message = null)
        {
            return new Response<T>(data,true, message);
        }
        public Response<T> Fail(T data, string message = null)
        {
            return new Response<T>(data, false, message);
        }
        private Response(T data,bool isSucceeded,string message = null)
        {
            Data = data;
            IsSucceeded = isSucceeded;
            Message = message;
        }

    }
}
