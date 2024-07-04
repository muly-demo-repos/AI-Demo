using ConstructionManagementService.Infrastructure;

namespace ConstructionManagementService.APIs;

public class ProjectsService : ProjectsServiceBase
{
    public ProjectsService(ConstructionManagementServiceDbContext context)
        : base(context) { }
}
