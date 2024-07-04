using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net1.APIs;
using Net1.APIs.Common;
using Net1.APIs.Dtos;
using Net1.APIs.Errors;

namespace Net1.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class WomenControllerBase : ControllerBase
{
    protected readonly IWomenService _service;

    public WomenControllerBase(IWomenService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Woman
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Woman>> CreateWoman(WomanCreateInput input)
    {
        var woman = await _service.CreateWoman(input);

        return CreatedAtAction(nameof(Woman), new { id = woman.Id }, woman);
    }

    /// <summary>
    /// Delete one Woman
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteWoman([FromRoute()] WomanWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteWoman(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Women
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Woman>>> Women([FromQuery()] WomanFindManyArgs filter)
    {
        return Ok(await _service.Women(filter));
    }

    /// <summary>
    /// Get one Woman
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Woman>> Woman([FromRoute()] WomanWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Woman(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Woman
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateWoman(
        [FromRoute()] WomanWhereUniqueInput uniqueId,
        [FromQuery()] WomanUpdateInput womanUpdateDto
    )
    {
        try
        {
            await _service.UpdateWoman(uniqueId, womanUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Meta data about Woman records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> WomenMeta([FromQuery()] WomanFindManyArgs filter)
    {
        return Ok(await _service.WomenMeta(filter));
    }
}
