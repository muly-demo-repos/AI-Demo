using Net1.Infrastructure;

namespace Net1.APIs;

public class MorsService : MorsServiceBase
{
    public MorsService(Net1DbContext context)
        : base(context) { }
}
