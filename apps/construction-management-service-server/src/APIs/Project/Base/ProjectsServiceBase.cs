using ConstructionManagementService.APIs;
using ConstructionManagementService.APIs.Common;
using ConstructionManagementService.APIs.Dtos;
using ConstructionManagementService.APIs.Errors;
using ConstructionManagementService.APIs.Extensions;
using ConstructionManagementService.Infrastructure;
using ConstructionManagementService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructionManagementService.APIs;

public abstract class ProjectsServiceBase : IProjectsService
{
    protected readonly ConstructionManagementServiceDbContext _context;

    public ProjectsServiceBase(ConstructionManagementServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Project
    /// </summary>
    public async Task<Project> CreateProject(ProjectCreateInput createDto)
    {
        var project = new ProjectDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Description = createDto.Description,
            EndDate = createDto.EndDate,
            ProjectName = createDto.ProjectName,
            StartDate = createDto.StartDate,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            project.Id = createDto.Id;
        }
        if (createDto.Tasks != null)
        {
            project.Tasks = await _context
                .Tasks.Where(task => createDto.Tasks.Select(t => t.Id).Contains(task.Id))
                .ToListAsync();
        }

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ProjectDbModel>(project.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Project
    /// </summary>
    public async Task DeleteProject(ProjectWhereUniqueInput uniqueId)
    {
        var project = await _context.Projects.FindAsync(uniqueId.Id);
        if (project == null)
        {
            throw new NotFoundException();
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Projects
    /// </summary>
    public async Task<List<Project>> Projects(ProjectFindManyArgs findManyArgs)
    {
        var projects = await _context
            .Projects.Include(x => x.Tasks)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return projects.ConvertAll(project => project.ToDto());
    }

    /// <summary>
    /// Get one Project
    /// </summary>
    public async Task<Project> Project(ProjectWhereUniqueInput uniqueId)
    {
        var projects = await this.Projects(
            new ProjectFindManyArgs { Where = new ProjectWhereInput { Id = uniqueId.Id } }
        );
        var project = projects.FirstOrDefault();
        if (project == null)
        {
            throw new NotFoundException();
        }

        return project;
    }

    /// <summary>
    /// Connect multiple Tasks records to Project
    /// </summary>
    public async Task ConnectTasks(ProjectWhereUniqueInput uniqueId, TaskWhereUniqueInput[] tasksId)
    {
        var project = await _context
            .Projects.Include(x => x.Tasks)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (project == null)
        {
            throw new NotFoundException();
        }

        var tasks = await _context
            .Tasks.Where(t => tasksId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (tasks.Count == 0)
        {
            throw new NotFoundException();
        }

        var tasksToConnect = tasks.Except(project.Tasks);

        foreach (var task in tasksToConnect)
        {
            project.Tasks.Add(task);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Tasks records from Project
    /// </summary>
    public async Task DisconnectTasks(
        ProjectWhereUniqueInput uniqueId,
        TaskWhereUniqueInput[] tasksId
    )
    {
        var project = await _context
            .Projects.Include(x => x.Tasks)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (project == null)
        {
            throw new NotFoundException();
        }

        var tasks = await _context
            .Tasks.Where(t => tasksId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var task in tasks)
        {
            project.Tasks?.Remove(task);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Tasks records for Project
    /// </summary>
    public async Task<List<Task>> FindTasks(
        ProjectWhereUniqueInput uniqueId,
        TaskFindManyArgs projectFindManyArgs
    )
    {
        var tasks = await _context
            .Tasks.Where(m => m.ProjectId == uniqueId.Id)
            .ApplyWhere(projectFindManyArgs.Where)
            .ApplySkip(projectFindManyArgs.Skip)
            .ApplyTake(projectFindManyArgs.Take)
            .ApplyOrderBy(projectFindManyArgs.SortBy)
            .ToListAsync();

        return tasks.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Meta data about Project records
    /// </summary>
    public async Task<MetadataDto> ProjectsMeta(ProjectFindManyArgs findManyArgs)
    {
        var count = await _context.Projects.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update multiple Tasks records for Project
    /// </summary>
    public async Task UpdateTasks(ProjectWhereUniqueInput uniqueId, TaskWhereUniqueInput[] tasksId)
    {
        var project = await _context
            .Projects.Include(t => t.Tasks)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (project == null)
        {
            throw new NotFoundException();
        }

        var tasks = await _context
            .Tasks.Where(a => tasksId.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (tasks.Count == 0)
        {
            throw new NotFoundException();
        }

        project.Tasks = tasks;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update one Project
    /// </summary>
    public async Task UpdateProject(ProjectWhereUniqueInput uniqueId, ProjectUpdateInput updateDto)
    {
        var project = updateDto.ToModel(uniqueId);

        if (updateDto.Tasks != null)
        {
            project.Tasks = await _context
                .Tasks.Where(task => updateDto.Tasks.Select(t => t).Contains(task.Id))
                .ToListAsync();
        }

        _context.Entry(project).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Projects.Any(e => e.Id == project.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
