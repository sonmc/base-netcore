namespace BaseNetCore.Src.Helper
{
  public class Response
  { 
    public string Status { get; set; }
    public object Result { get; set; }
    public Response(string status, object data)
    {
      Status = status;
      Result = data;
    }
  }
}
