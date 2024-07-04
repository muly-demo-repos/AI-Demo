using Net1.Infrastructure;

namespace Net1.APIs;

public class MuliesService : MuliesServiceBase
{
    public MuliesService(Net1DbContext context)
        : base(context) { }
}
