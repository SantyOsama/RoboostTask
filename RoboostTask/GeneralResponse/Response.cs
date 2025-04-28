namespace RoboostTask.GeneralResponse
{
    public class Response<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Succeeded { get; set; }
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

    }
}
