using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net1.APIs;
using Net1.APIs.Common;
using Net1.APIs.Dtos;
using Net1.APIs.Errors;

namespace Net1.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class HaimsControllerBase : ControllerBase
{
    protected readonly IHaimsService _service;

    public HaimsControllerBase(IHaimsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Haims
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Haim>> CreateHaim(HaimCreateInput input)
    {
        var haim = await _service.CreateHaim(input);

        return CreatedAtAction(nameof(Haim), new { id = haim.Id }, haim);
    }

    /// <summary>
    /// Delete one Haims
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteHaim([FromRoute()] HaimWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteHaim(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Haims
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Haim>>> Haims([FromQuery()] HaimFindManyArgs filter)
    {
        return Ok(await _service.Haims(filter));
    }

    /// <summary>
    /// Get one Haims
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Haim>> Haim([FromRoute()] HaimWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Haim(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about Haims records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> HaimsMeta([FromQuery()] HaimFindManyArgs filter)
    {
        return Ok(await _service.HaimsMeta(filter));
    }

    /// <summary>
    /// Update one Haims
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateHaim(
        [FromRoute()] HaimWhereUniqueInput uniqueId,
        [FromQuery()] HaimUpdateInput haimUpdateDto
    )
    {
        try
        {
            await _service.UpdateHaim(uniqueId, haimUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
