using Microsoft.AspNetCore.Mvc;

namespace RealEstateCrm.APIs;

[ApiController()]
public class AgentAssignmentsController : AgentAssignmentsControllerBase
{
    public AgentAssignmentsController(IAgentAssignmentsService service)
        : base(service) { }
}
