using Microsoft.AspNetCore.Mvc;
using RealEstateCrm.APIs;
using RealEstateCrm.APIs.Common;
using RealEstateCrm.APIs.Dtos;
using RealEstateCrm.APIs.Errors;

namespace RealEstateCrm.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class PropertiesControllerBase : ControllerBase
{
    protected readonly IPropertiesService _service;

    public PropertiesControllerBase(IPropertiesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Property
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Property>> CreateProperty(PropertyCreateInput input)
    {
        var property = await _service.CreateProperty(input);

        return CreatedAtAction(nameof(Property), new { id = property.Id }, property);
    }

    /// <summary>
    /// Delete one Property
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteProperty([FromRoute()] PropertyWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteProperty(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Properties
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Property>>> Properties(
        [FromQuery()] PropertyFindManyArgs filter
    )
    {
        return Ok(await _service.Properties(filter));
    }

    /// <summary>
    /// Get one Property
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Property>> Property(
        [FromRoute()] PropertyWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Property(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about Property records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> PropertiesMeta(
        [FromQuery()] PropertyFindManyArgs filter
    )
    {
        return Ok(await _service.PropertiesMeta(filter));
    }

    /// <summary>
    /// Connect multiple AgentAssignments records to Property
    /// </summary>
    [HttpPost("{Id}/agentAssignments")]
    public async Task<ActionResult> ConnectAgentAssignments(
        [FromRoute()] PropertyWhereUniqueInput uniqueId,
        [FromQuery()] AgentAssignmentWhereUniqueInput[] agentAssignmentsId
    )
    {
        try
        {
            await _service.ConnectAgentAssignments(uniqueId, agentAssignmentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple AgentAssignments records from Property
    /// </summary>
    [HttpDelete("{Id}/agentAssignments")]
    public async Task<ActionResult> DisconnectAgentAssignments(
        [FromRoute()] PropertyWhereUniqueInput uniqueId,
        [FromBody()] AgentAssignmentWhereUniqueInput[] agentAssignmentsId
    )
    {
        try
        {
            await _service.DisconnectAgentAssignments(uniqueId, agentAssignmentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple AgentAssignments records for Property
    /// </summary>
    [HttpGet("{Id}/agentAssignments")]
    public async Task<ActionResult<List<AgentAssignment>>> FindAgentAssignments(
        [FromRoute()] PropertyWhereUniqueInput uniqueId,
        [FromQuery()] AgentAssignmentFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindAgentAssignments(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple AgentAssignments records for Property
    /// </summary>
    [HttpPatch("{Id}/agentAssignments")]
    public async Task<ActionResult> UpdateAgentAssignments(
        [FromRoute()] PropertyWhereUniqueInput uniqueId,
        [FromBody()] AgentAssignmentWhereUniqueInput[] agentAssignmentsId
    )
    {
        try
        {
            await _service.UpdateAgentAssignments(uniqueId, agentAssignmentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Update one Property
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateProperty(
        [FromRoute()] PropertyWhereUniqueInput uniqueId,
        [FromQuery()] PropertyUpdateInput propertyUpdateDto
    )
    {
        try
        {
            await _service.UpdateProperty(uniqueId, propertyUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
