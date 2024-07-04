using ConstructionManagementService.APIs;
using ConstructionManagementService.APIs.Common;
using ConstructionManagementService.APIs.Dtos;
using ConstructionManagementService.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagementService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ProjectsControllerBase : ControllerBase
{
    protected readonly IProjectsService _service;

    public ProjectsControllerBase(IProjectsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Project
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Project>> CreateProject(ProjectCreateInput input)
    {
        var project = await _service.CreateProject(input);

        return CreatedAtAction(nameof(Project), new { id = project.Id }, project);
    }

    /// <summary>
    /// Delete one Project
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteProject([FromRoute()] ProjectWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteProject(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Projects
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Project>>> Projects(
        [FromQuery()] ProjectFindManyArgs filter
    )
    {
        return Ok(await _service.Projects(filter));
    }

    /// <summary>
    /// Get one Project
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Project>> Project([FromRoute()] ProjectWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Project(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Connect multiple Tasks records to Project
    /// </summary>
    [HttpPost("{Id}/tasks")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectTasks(
        [FromRoute()] ProjectWhereUniqueInput uniqueId,
        [FromQuery()] TaskWhereUniqueInput[] tasksId
    )
    {
        try
        {
            await _service.ConnectTasks(uniqueId, tasksId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Tasks records from Project
    /// </summary>
    [HttpDelete("{Id}/tasks")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectTasks(
        [FromRoute()] ProjectWhereUniqueInput uniqueId,
        [FromBody()] TaskWhereUniqueInput[] tasksId
    )
    {
        try
        {
            await _service.DisconnectTasks(uniqueId, tasksId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Tasks records for Project
    /// </summary>
    [HttpGet("{Id}/tasks")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Task>>> FindTasks(
        [FromRoute()] ProjectWhereUniqueInput uniqueId,
        [FromQuery()] TaskFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindTasks(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about Project records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ProjectsMeta(
        [FromQuery()] ProjectFindManyArgs filter
    )
    {
        return Ok(await _service.ProjectsMeta(filter));
    }

    /// <summary>
    /// Update multiple Tasks records for Project
    /// </summary>
    [HttpPatch("{Id}/tasks")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateTasks(
        [FromRoute()] ProjectWhereUniqueInput uniqueId,
        [FromBody()] TaskWhereUniqueInput[] tasksId
    )
    {
        try
        {
            await _service.UpdateTasks(uniqueId, tasksId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Update one Project
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateProject(
        [FromRoute()] ProjectWhereUniqueInput uniqueId,
        [FromQuery()] ProjectUpdateInput projectUpdateDto
    )
    {
        try
        {
            await _service.UpdateProject(uniqueId, projectUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
