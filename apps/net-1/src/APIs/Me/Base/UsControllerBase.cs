using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net1.APIs;
using Net1.APIs.Common;
using Net1.APIs.Dtos;
using Net1.APIs.Errors;

namespace Net1.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class UsControllerBase : ControllerBase
{
    protected readonly IUsService _service;

    public UsControllerBase(IUsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one us
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Me>> CreateMe(MeCreateInput input)
    {
        var me = await _service.CreateMe(input);

        return CreatedAtAction(nameof(Me), new { id = me.Id }, me);
    }

    /// <summary>
    /// Delete one us
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteMe([FromRoute()] MeWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteMe(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many us
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Me>>> Us([FromQuery()] MeFindManyArgs filter)
    {
        return Ok(await _service.Us(filter));
    }

    /// <summary>
    /// Get one us
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Me>> Me([FromRoute()] MeWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Me(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one us
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateMe(
        [FromRoute()] MeWhereUniqueInput uniqueId,
        [FromQuery()] MeUpdateInput meUpdateDto
    )
    {
        try
        {
            await _service.UpdateMe(uniqueId, meUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Meta data about us records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> UsMeta([FromQuery()] MeFindManyArgs filter)
    {
        return Ok(await _service.UsMeta(filter));
    }
}
