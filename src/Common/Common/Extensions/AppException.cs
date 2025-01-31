using System.Net;

namespace Common.Extensions;

public class AppException : Exception
{
    public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;
    public AppException(string message) : base(message) { }
    public AppException(HttpStatusCode status, string message) : base(message)
    {
        Status = status;
    }
}