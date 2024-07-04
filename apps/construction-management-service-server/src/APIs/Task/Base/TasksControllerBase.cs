using ConstructionManagementService.APIs;
using ConstructionManagementService.APIs.Common;
using ConstructionManagementService.APIs.Dtos;
using ConstructionManagementService.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagementService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class TasksControllerBase : ControllerBase
{
    protected readonly ITasksService _service;

    public TasksControllerBase(ITasksService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Task
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Task>> CreateTask(TaskCreateInput input)
    {
        var task = await _service.CreateTask(input);

        return CreatedAtAction(nameof(Task), new { id = task.Id }, task);
    }

    /// <summary>
    /// Delete one Task
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteTask([FromRoute()] TaskWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteTask(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Tasks
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Task>>> Tasks([FromQuery()] TaskFindManyArgs filter)
    {
        return Ok(await _service.Tasks(filter));
    }

    /// <summary>
    /// Get one Task
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Task>> Task([FromRoute()] TaskWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Task(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Get a Project record for Task
    /// </summary>
    [HttpGet("{Id}/projects")]
    public async Task<ActionResult<List<Project>>> GetProject(
        [FromRoute()] TaskWhereUniqueInput uniqueId
    )
    {
        var project = await _service.GetProject(uniqueId);
        return Ok(project);
    }

    /// <summary>
    /// Meta data about Task records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> TasksMeta([FromQuery()] TaskFindManyArgs filter)
    {
        return Ok(await _service.TasksMeta(filter));
    }

    /// <summary>
    /// Update one Task
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateTask(
        [FromRoute()] TaskWhereUniqueInput uniqueId,
        [FromQuery()] TaskUpdateInput taskUpdateDto
    )
    {
        try
        {
            await _service.UpdateTask(uniqueId, taskUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
