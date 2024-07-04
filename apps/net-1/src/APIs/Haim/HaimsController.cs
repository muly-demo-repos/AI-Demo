using Microsoft.AspNetCore.Mvc;

namespace Net1.APIs;

[ApiController()]
public class HaimsController : HaimsControllerBase
{
    public HaimsController(IHaimsService service)
        : base(service) { }
}
