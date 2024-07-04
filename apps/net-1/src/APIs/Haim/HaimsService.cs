using Net1.Infrastructure;

namespace Net1.APIs;

public class HaimsService : HaimsServiceBase
{
    public HaimsService(Net1DbContext context)
        : base(context) { }
}
