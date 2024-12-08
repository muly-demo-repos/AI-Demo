using Microsoft.AspNetCore.Mvc;

namespace Net1.APIs;

[ApiController()]
public class MorsController : MorsControllerBase
{
    public MorsController(IMorsService service)
        : base(service) { }
}
