using Microsoft.AspNetCore.Mvc;

namespace RealEstateCrm.APIs;

[ApiController()]
public class AppointmentsController : AppointmentsControllerBase
{
    public AppointmentsController(IAppointmentsService service)
        : base(service) { }
}
