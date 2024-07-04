using Microsoft.AspNetCore.Mvc;

namespace RealEstateCrm.APIs;

[ApiController()]
public class PropertiesController : PropertiesControllerBase
{
    public PropertiesController(IPropertiesService service)
        : base(service) { }
}
