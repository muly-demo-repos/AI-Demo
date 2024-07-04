using Net1.Infrastructure;

namespace Net1.APIs;

public class ShelvesService : ShelvesServiceBase
{
    public ShelvesService(Net1DbContext context)
        : base(context) { }
}
