using ConstructionManagementService.Infrastructure;

namespace ConstructionManagementService.APIs;

public class TasksService : TasksServiceBase
{
    public TasksService(ConstructionManagementServiceDbContext context)
        : base(context) { }
}
