using ConstructionManagementService.APIs;
using ConstructionManagementService.APIs.Common;
using ConstructionManagementService.APIs.Dtos;
using ConstructionManagementService.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagementService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class WorkersControllerBase : ControllerBase
{
    protected readonly IWorkersService _service;

    public WorkersControllerBase(IWorkersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Worker
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Worker>> CreateWorker(WorkerCreateInput input)
    {
        var worker = await _service.CreateWorker(input);

        return CreatedAtAction(nameof(Worker), new { id = worker.Id }, worker);
    }

    /// <summary>
    /// Delete one Worker
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteWorker([FromRoute()] WorkerWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteWorker(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Workers
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Worker>>> Workers([FromQuery()] WorkerFindManyArgs filter)
    {
        return Ok(await _service.Workers(filter));
    }

    /// <summary>
    /// Get one Worker
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Worker>> Worker([FromRoute()] WorkerWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Worker(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Worker
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateWorker(
        [FromRoute()] WorkerWhereUniqueInput uniqueId,
        [FromQuery()] WorkerUpdateInput workerUpdateDto
    )
    {
        try
        {
            await _service.UpdateWorker(uniqueId, workerUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Meta data about Worker records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> WorkersMeta(
        [FromQuery()] WorkerFindManyArgs filter
    )
    {
        return Ok(await _service.WorkersMeta(filter));
    }
}
