using System.Runtime.Serialization;

namespace HoneyShop.Core.Excpetions;

public class HoneyAccessDeniedException:HoneyException
{
    public HoneyAccessDeniedException()
        : base("Access denied.", 403) { }

    protected HoneyAccessDeniedException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}