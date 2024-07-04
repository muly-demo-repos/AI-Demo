using ConstructionManagementService.APIs.Common;
using ConstructionManagementService.APIs.Dtos;

namespace ConstructionManagementService.APIs;

public interface IProjectsService
{
    /// <summary>
    /// Create one Project
    /// </summary>
    public Task<Project> CreateProject(ProjectCreateInput project);

    /// <summary>
    /// Delete one Project
    /// </summary>
    public Task DeleteProject(ProjectWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Projects
    /// </summary>
    public Task<List<Project>> Projects(ProjectFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Project
    /// </summary>
    public Task<Project> Project(ProjectWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple Tasks records to Project
    /// </summary>
    public Task ConnectTasks(ProjectWhereUniqueInput uniqueId, TaskWhereUniqueInput[] tasksId);

    /// <summary>
    /// Disconnect multiple Tasks records from Project
    /// </summary>
    public Task DisconnectTasks(ProjectWhereUniqueInput uniqueId, TaskWhereUniqueInput[] tasksId);

    /// <summary>
    /// Find multiple Tasks records for Project
    /// </summary>
    public Task<List<Task>> FindTasks(
        ProjectWhereUniqueInput uniqueId,
        TaskFindManyArgs TaskFindManyArgs
    );

    /// <summary>
    /// Meta data about Project records
    /// </summary>
    public Task<MetadataDto> ProjectsMeta(ProjectFindManyArgs findManyArgs);

    /// <summary>
    /// Update multiple Tasks records for Project
    /// </summary>
    public Task UpdateTasks(ProjectWhereUniqueInput uniqueId, TaskWhereUniqueInput[] tasksId);

    /// <summary>
    /// Update one Project
    /// </summary>
    public Task UpdateProject(ProjectWhereUniqueInput uniqueId, ProjectUpdateInput updateDto);
}
