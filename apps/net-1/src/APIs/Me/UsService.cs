using Net1.Infrastructure;

namespace Net1.APIs;

public class UsService : UsServiceBase
{
    public UsService(Net1DbContext context)
        : base(context) { }
}
