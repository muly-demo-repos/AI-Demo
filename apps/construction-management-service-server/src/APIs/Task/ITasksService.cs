using ConstructionManagementService.APIs.Common;
using ConstructionManagementService.APIs.Dtos;

namespace ConstructionManagementService.APIs;

public interface ITasksService
{
    /// <summary>
    /// Create one Task
    /// </summary>
    public Task<Task> CreateTask(TaskCreateInput task);

    /// <summary>
    /// Delete one Task
    /// </summary>
    public Task DeleteTask(TaskWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Tasks
    /// </summary>
    public Task<List<Task>> Tasks(TaskFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Task
    /// </summary>
    public Task<Task> Task(TaskWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Project record for Task
    /// </summary>
    public Task<Project> GetProject(TaskWhereUniqueInput uniqueId);

    /// <summary>
    /// Meta data about Task records
    /// </summary>
    public Task<MetadataDto> TasksMeta(TaskFindManyArgs findManyArgs);

    /// <summary>
    /// Update one Task
    /// </summary>
    public Task UpdateTask(TaskWhereUniqueInput uniqueId, TaskUpdateInput updateDto);
}
