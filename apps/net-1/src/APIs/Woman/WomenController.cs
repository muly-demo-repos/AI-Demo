using Microsoft.AspNetCore.Mvc;

namespace Net1.APIs;

[ApiController()]
public class WomenController : WomenControllerBase
{
    public WomenController(IWomenService service)
        : base(service) { }
}
