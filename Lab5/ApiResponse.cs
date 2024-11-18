using System.Net;

namespace Lab5;

internal class ApiResponse<T>(string message, HttpStatusCode statusCode)
{
    public ApiResponse(string message, List<T> data, HttpStatusCode statusCode) : this(message, statusCode)
    {
        Data = data;
    }


    public string Message { get; } = message;

    public List<T>? Data { get; }

    public HttpStatusCode StatusCode { get; } = statusCode;
}
