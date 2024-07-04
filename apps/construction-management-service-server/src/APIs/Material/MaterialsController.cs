using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagementService.APIs;

[ApiController()]
public class MaterialsController : MaterialsControllerBase
{
    public MaterialsController(IMaterialsService service)
        : base(service) { }
}
