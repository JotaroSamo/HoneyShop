using System.Runtime.Serialization;

namespace HoneyShop.Core.Excpetions;

public class HoneyException : Exception
{
    public HoneyException(string message, int statusCode = 500)
        : base(message)
    {
        StatusCode = statusCode;
    }

    protected HoneyException(string message, object obj, int statusCode = 500)
        : base(message)
    {
        StatusCode = statusCode;
        Object = obj;
    }

    protected HoneyException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }

    public int StatusCode { get; }

    public object Object { get; }
}