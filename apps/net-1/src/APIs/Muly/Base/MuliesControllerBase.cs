using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net1.APIs;
using Net1.APIs.Common;
using Net1.APIs.Dtos;
using Net1.APIs.Errors;

namespace Net1.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class MuliesControllerBase : ControllerBase
{
    protected readonly IMuliesService _service;

    public MuliesControllerBase(IMuliesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Muly
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Muly>> CreateMuly(MulyCreateInput input)
    {
        var muly = await _service.CreateMuly(input);

        return CreatedAtAction(nameof(Muly), new { id = muly.Id }, muly);
    }

    /// <summary>
    /// Delete one Muly
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteMuly([FromRoute()] MulyWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteMuly(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Mulies
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Muly>>> Mulies([FromQuery()] MulyFindManyArgs filter)
    {
        return Ok(await _service.Mulies(filter));
    }

    /// <summary>
    /// Get one Muly
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Muly>> Muly([FromRoute()] MulyWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Muly(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about Muly records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> MuliesMeta([FromQuery()] MulyFindManyArgs filter)
    {
        return Ok(await _service.MuliesMeta(filter));
    }

    /// <summary>
    /// Update one Muly
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateMuly(
        [FromRoute()] MulyWhereUniqueInput uniqueId,
        [FromQuery()] MulyUpdateInput mulyUpdateDto
    )
    {
        try
        {
            await _service.UpdateMuly(uniqueId, mulyUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
