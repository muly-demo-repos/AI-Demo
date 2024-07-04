using ConstructionManagementService.Infrastructure;

namespace ConstructionManagementService.APIs;

public class WorkersService : WorkersServiceBase
{
    public WorkersService(ConstructionManagementServiceDbContext context)
        : base(context) { }
}
