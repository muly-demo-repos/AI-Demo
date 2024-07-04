using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net1.APIs;
using Net1.APIs.Common;
using Net1.APIs.Dtos;
using Net1.APIs.Errors;

namespace Net1.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ShelvesControllerBase : ControllerBase
{
    protected readonly IShelvesService _service;

    public ShelvesControllerBase(IShelvesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one shelf
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Shelf>> CreateShelf(ShelfCreateInput input)
    {
        var shelf = await _service.CreateShelf(input);

        return CreatedAtAction(nameof(Shelf), new { id = shelf.Id }, shelf);
    }

    /// <summary>
    /// Delete one shelf
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteShelf([FromRoute()] ShelfWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteShelf(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many shelves
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Shelf>>> Shelves([FromQuery()] ShelfFindManyArgs filter)
    {
        return Ok(await _service.Shelves(filter));
    }

    /// <summary>
    /// Get one shelf
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Shelf>> Shelf([FromRoute()] ShelfWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Shelf(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about shelf records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ShelvesMeta([FromQuery()] ShelfFindManyArgs filter)
    {
        return Ok(await _service.ShelvesMeta(filter));
    }

    /// <summary>
    /// Update one shelf
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateShelf(
        [FromRoute()] ShelfWhereUniqueInput uniqueId,
        [FromQuery()] ShelfUpdateInput shelfUpdateDto
    )
    {
        try
        {
            await _service.UpdateShelf(uniqueId, shelfUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
