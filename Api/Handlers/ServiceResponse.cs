using System.Text.Json.Serialization;

namespace PeoplesPartnership.ApiRefactor.Handlers;

public class ServiceResponse<T>
{
    public T Data { get; set; }

    public bool Success { get; set; } = false;
    
    public ServiceResponseType Type { get; set; }

    public string Message { get; set; } = null;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ServiceResponseType
{
    Success,
    NotFound,
    BadRequest
}