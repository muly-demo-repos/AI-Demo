using RealEstateCrm.Infrastructure;

namespace RealEstateCrm.APIs;

public class ClientsService : ClientsServiceBase
{
    public ClientsService(RealEstateCrmDbContext context)
        : base(context) { }
}
