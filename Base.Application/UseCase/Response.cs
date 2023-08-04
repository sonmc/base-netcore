namespace Base.Application.UseCase
{ 
    public class Response
    {
        public string Status { get; set; }
        public object Result { get; set; }
        public Response() { }
        public Response(string status, object data)
        {
            Status = status;
            Result = data;
        }
    }

    public class ResponsePresenter
    {
        public object Items { get; set; }
        public bool HasNextPage { get; set; }
    }
}
