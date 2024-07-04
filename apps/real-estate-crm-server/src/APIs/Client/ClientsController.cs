using Microsoft.AspNetCore.Mvc;

namespace RealEstateCrm.APIs;

[ApiController()]
public class ClientsController : ClientsControllerBase
{
    public ClientsController(IClientsService service)
        : base(service) { }
}
