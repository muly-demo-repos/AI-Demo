using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagementService.APIs;

[ApiController()]
public class ProjectsController : ProjectsControllerBase
{
    public ProjectsController(IProjectsService service)
        : base(service) { }
}
