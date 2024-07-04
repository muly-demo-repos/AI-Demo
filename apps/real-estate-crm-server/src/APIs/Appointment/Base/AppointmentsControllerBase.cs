using Microsoft.AspNetCore.Mvc;
using RealEstateCrm.APIs;
using RealEstateCrm.APIs.Common;
using RealEstateCrm.APIs.Dtos;
using RealEstateCrm.APIs.Errors;

namespace RealEstateCrm.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class AppointmentsControllerBase : ControllerBase
{
    protected readonly IAppointmentsService _service;

    public AppointmentsControllerBase(IAppointmentsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get a Client record for Appointment
    /// </summary>
    [HttpGet("{Id}/clients")]
    public async Task<ActionResult<List<Client>>> GetClient(
        [FromRoute()] AppointmentWhereUniqueInput uniqueId
    )
    {
        var client = await _service.GetClient(uniqueId);
        return Ok(client);
    }

    /// <summary>
    /// Meta data about Appointment records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> AppointmentsMeta(
        [FromQuery()] AppointmentFindManyArgs filter
    )
    {
        return Ok(await _service.AppointmentsMeta(filter));
    }

    /// <summary>
    /// Create one Appointment
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Appointment>> CreateAppointment(AppointmentCreateInput input)
    {
        var appointment = await _service.CreateAppointment(input);

        return CreatedAtAction(nameof(Appointment), new { id = appointment.Id }, appointment);
    }

    /// <summary>
    /// Delete one Appointment
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteAppointment(
        [FromRoute()] AppointmentWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteAppointment(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Appointments
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Appointment>>> Appointments(
        [FromQuery()] AppointmentFindManyArgs filter
    )
    {
        return Ok(await _service.Appointments(filter));
    }

    /// <summary>
    /// Get one Appointment
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Appointment>> Appointment(
        [FromRoute()] AppointmentWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Appointment(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Appointment
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateAppointment(
        [FromRoute()] AppointmentWhereUniqueInput uniqueId,
        [FromQuery()] AppointmentUpdateInput appointmentUpdateDto
    )
    {
        try
        {
            await _service.UpdateAppointment(uniqueId, appointmentUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
