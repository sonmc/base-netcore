using System.Text.Json;

namespace Base.Utils
{
    public class ExceptionUtil
    {
        public struct ResponseHeaders
        {
            public const string TOKEN_EXPIRED = "Token-Expired";
        }
    }

    public class ErrorDetails
    { 
        public string message { get; set; }
        public string Message { get; set; }
        public string? Title { get; set; }
        public Object? Details { get; set; }

        public ErrorDetails()
        {
            Title = "";
            message = "";
            Message = "";
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
