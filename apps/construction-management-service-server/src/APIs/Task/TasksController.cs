using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagementService.APIs;

[ApiController()]
public class TasksController : TasksControllerBase
{
    public TasksController(ITasksService service)
        : base(service) { }
}
