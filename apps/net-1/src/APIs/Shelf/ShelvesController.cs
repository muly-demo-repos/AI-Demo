using Microsoft.AspNetCore.Mvc;

namespace Net1.APIs;

[ApiController()]
public class ShelvesController : ShelvesControllerBase
{
    public ShelvesController(IShelvesService service)
        : base(service) { }
}
