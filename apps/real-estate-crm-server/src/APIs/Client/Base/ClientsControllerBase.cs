using Microsoft.AspNetCore.Mvc;
using RealEstateCrm.APIs;
using RealEstateCrm.APIs.Common;
using RealEstateCrm.APIs.Dtos;
using RealEstateCrm.APIs.Errors;

namespace RealEstateCrm.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ClientsControllerBase : ControllerBase
{
    protected readonly IClientsService _service;

    public ClientsControllerBase(IClientsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Connect multiple Appointments records to Client
    /// </summary>
    [HttpPost("{Id}/appointments")]
    public async Task<ActionResult> ConnectAppointments(
        [FromRoute()] ClientWhereUniqueInput uniqueId,
        [FromQuery()] AppointmentWhereUniqueInput[] appointmentsId
    )
    {
        try
        {
            await _service.ConnectAppointments(uniqueId, appointmentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Appointments records from Client
    /// </summary>
    [HttpDelete("{Id}/appointments")]
    public async Task<ActionResult> DisconnectAppointments(
        [FromRoute()] ClientWhereUniqueInput uniqueId,
        [FromBody()] AppointmentWhereUniqueInput[] appointmentsId
    )
    {
        try
        {
            await _service.DisconnectAppointments(uniqueId, appointmentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Appointments records for Client
    /// </summary>
    [HttpGet("{Id}/appointments")]
    public async Task<ActionResult<List<Appointment>>> FindAppointments(
        [FromRoute()] ClientWhereUniqueInput uniqueId,
        [FromQuery()] AppointmentFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindAppointments(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about Client records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ClientsMeta(
        [FromQuery()] ClientFindManyArgs filter
    )
    {
        return Ok(await _service.ClientsMeta(filter));
    }

    /// <summary>
    /// Update multiple Appointments records for Client
    /// </summary>
    [HttpPatch("{Id}/appointments")]
    public async Task<ActionResult> UpdateAppointments(
        [FromRoute()] ClientWhereUniqueInput uniqueId,
        [FromBody()] AppointmentWhereUniqueInput[] appointmentsId
    )
    {
        try
        {
            await _service.UpdateAppointments(uniqueId, appointmentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Create one Client
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Client>> CreateClient(ClientCreateInput input)
    {
        var client = await _service.CreateClient(input);

        return CreatedAtAction(nameof(Client), new { id = client.Id }, client);
    }

    /// <summary>
    /// Delete one Client
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteClient([FromRoute()] ClientWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteClient(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Clients
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Client>>> Clients([FromQuery()] ClientFindManyArgs filter)
    {
        return Ok(await _service.Clients(filter));
    }

    /// <summary>
    /// Get one Client
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Client>> Client([FromRoute()] ClientWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Client(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Client
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateClient(
        [FromRoute()] ClientWhereUniqueInput uniqueId,
        [FromQuery()] ClientUpdateInput clientUpdateDto
    )
    {
        try
        {
            await _service.UpdateClient(uniqueId, clientUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
