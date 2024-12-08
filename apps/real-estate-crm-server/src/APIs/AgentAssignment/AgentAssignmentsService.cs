using RealEstateCrm.Infrastructure;

namespace RealEstateCrm.APIs;

public class AgentAssignmentsService : AgentAssignmentsServiceBase
{
    public AgentAssignmentsService(RealEstateCrmDbContext context)
        : base(context) { }
}
