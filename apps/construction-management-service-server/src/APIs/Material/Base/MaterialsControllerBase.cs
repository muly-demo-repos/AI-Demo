using ConstructionManagementService.APIs;
using ConstructionManagementService.APIs.Common;
using ConstructionManagementService.APIs.Dtos;
using ConstructionManagementService.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagementService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class MaterialsControllerBase : ControllerBase
{
    protected readonly IMaterialsService _service;

    public MaterialsControllerBase(IMaterialsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Material
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Material>> CreateMaterial(MaterialCreateInput input)
    {
        var material = await _service.CreateMaterial(input);

        return CreatedAtAction(nameof(Material), new { id = material.Id }, material);
    }

    /// <summary>
    /// Delete one Material
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteMaterial([FromRoute()] MaterialWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteMaterial(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Materials
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Material>>> Materials(
        [FromQuery()] MaterialFindManyArgs filter
    )
    {
        return Ok(await _service.Materials(filter));
    }

    /// <summary>
    /// Get one Material
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Material>> Material(
        [FromRoute()] MaterialWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Material(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about Material records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> MaterialsMeta(
        [FromQuery()] MaterialFindManyArgs filter
    )
    {
        return Ok(await _service.MaterialsMeta(filter));
    }

    /// <summary>
    /// Update one Material
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateMaterial(
        [FromRoute()] MaterialWhereUniqueInput uniqueId,
        [FromQuery()] MaterialUpdateInput materialUpdateDto
    )
    {
        try
        {
            await _service.UpdateMaterial(uniqueId, materialUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
