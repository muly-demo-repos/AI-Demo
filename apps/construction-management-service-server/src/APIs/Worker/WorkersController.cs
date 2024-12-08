using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagementService.APIs;

[ApiController()]
public class WorkersController : WorkersControllerBase
{
    public WorkersController(IWorkersService service)
        : base(service) { }
}
