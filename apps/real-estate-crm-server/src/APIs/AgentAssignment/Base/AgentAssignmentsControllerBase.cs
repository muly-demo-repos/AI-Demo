using Microsoft.AspNetCore.Mvc;
using RealEstateCrm.APIs;
using RealEstateCrm.APIs.Common;
using RealEstateCrm.APIs.Dtos;
using RealEstateCrm.APIs.Errors;

namespace RealEstateCrm.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class AgentAssignmentsControllerBase : ControllerBase
{
    protected readonly IAgentAssignmentsService _service;

    public AgentAssignmentsControllerBase(IAgentAssignmentsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get a Property record for AgentAssignment
    /// </summary>
    [HttpGet("{Id}/properties")]
    public async Task<ActionResult<List<Property>>> GetProperty(
        [FromRoute()] AgentAssignmentWhereUniqueInput uniqueId
    )
    {
        var property = await _service.GetProperty(uniqueId);
        return Ok(property);
    }

    /// <summary>
    /// Meta data about AgentAssignment records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> AgentAssignmentsMeta(
        [FromQuery()] AgentAssignmentFindManyArgs filter
    )
    {
        return Ok(await _service.AgentAssignmentsMeta(filter));
    }

    /// <summary>
    /// Create one AgentAssignment
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<AgentAssignment>> CreateAgentAssignment(
        AgentAssignmentCreateInput input
    )
    {
        var agentAssignment = await _service.CreateAgentAssignment(input);

        return CreatedAtAction(
            nameof(AgentAssignment),
            new { id = agentAssignment.Id },
            agentAssignment
        );
    }

    /// <summary>
    /// Delete one AgentAssignment
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteAgentAssignment(
        [FromRoute()] AgentAssignmentWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteAgentAssignment(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many AgentAssignments
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<AgentAssignment>>> AgentAssignments(
        [FromQuery()] AgentAssignmentFindManyArgs filter
    )
    {
        return Ok(await _service.AgentAssignments(filter));
    }

    /// <summary>
    /// Get one AgentAssignment
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<AgentAssignment>> AgentAssignment(
        [FromRoute()] AgentAssignmentWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.AgentAssignment(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one AgentAssignment
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateAgentAssignment(
        [FromRoute()] AgentAssignmentWhereUniqueInput uniqueId,
        [FromQuery()] AgentAssignmentUpdateInput agentAssignmentUpdateDto
    )
    {
        try
        {
            await _service.UpdateAgentAssignment(uniqueId, agentAssignmentUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
