using System.Net;

namespace Common.Dtos;

public class ResultDto(HttpStatusCode statusCode = HttpStatusCode.OK, string message = "با موفقیت انجام شد")
{
    public string Message { get; set; } = message;
    public HttpStatusCode StatusCode { get; set; } = statusCode;

    public bool IsSuccess => StatusCode == HttpStatusCode.OK;
}

public class ResultDto<T>(HttpStatusCode statusCode = HttpStatusCode.OK, string message = "با موفقیت انجام شد", T? data = default)
    : ResultDto(statusCode, message)
{
    public ResultDto(T data) : this()
    {
        Data = data;
    }

    public T? Data { get; set; } = data;
}