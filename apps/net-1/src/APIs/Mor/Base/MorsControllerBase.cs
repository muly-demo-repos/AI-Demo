using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net1.APIs;
using Net1.APIs.Common;
using Net1.APIs.Dtos;
using Net1.APIs.Errors;

namespace Net1.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class MorsControllerBase : ControllerBase
{
    protected readonly IMorsService _service;

    public MorsControllerBase(IMorsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one mor. $% ?
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Mor>> CreateMor(MorCreateInput input)
    {
        var mor = await _service.CreateMor(input);

        return CreatedAtAction(nameof(Mor), new { id = mor.Id }, mor);
    }

    /// <summary>
    /// Delete one mor. $% ?
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteMor([FromRoute()] MorWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteMor(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many mors
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Mor>>> Mors([FromQuery()] MorFindManyArgs filter)
    {
        return Ok(await _service.Mors(filter));
    }

    /// <summary>
    /// Get one mor. $% ?
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Mor>> Mor([FromRoute()] MorWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Mor(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about mor. $% ? records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> MorsMeta([FromQuery()] MorFindManyArgs filter)
    {
        return Ok(await _service.MorsMeta(filter));
    }

    /// <summary>
    /// Update one mor. $% ?
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateMor(
        [FromRoute()] MorWhereUniqueInput uniqueId,
        [FromQuery()] MorUpdateInput morUpdateDto
    )
    {
        try
        {
            await _service.UpdateMor(uniqueId, morUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
