using RealEstateCrm.Infrastructure;

namespace RealEstateCrm.APIs;

public class AppointmentsService : AppointmentsServiceBase
{
    public AppointmentsService(RealEstateCrmDbContext context)
        : base(context) { }
}
