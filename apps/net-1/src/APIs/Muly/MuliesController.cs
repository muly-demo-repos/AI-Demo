using Microsoft.AspNetCore.Mvc;

namespace Net1.APIs;

[ApiController()]
public class MuliesController : MuliesControllerBase
{
    public MuliesController(IMuliesService service)
        : base(service) { }
}
