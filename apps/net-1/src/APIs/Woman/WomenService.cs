using Net1.Infrastructure;

namespace Net1.APIs;

public class WomenService : WomenServiceBase
{
    public WomenService(Net1DbContext context)
        : base(context) { }
}
