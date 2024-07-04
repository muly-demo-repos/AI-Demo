using ConstructionManagementService.APIs.Dtos;
using ConstructionManagementService.Infrastructure.Models;

namespace ConstructionManagementService.APIs.Extensions;

public static class TasksExtensions
{
    public static Task ToDto(this TaskDbModel model)
    {
        return new Task
        {
            CreatedAt = model.CreatedAt,
            Description = model.Description,
            DueDate = model.DueDate,
            Id = model.Id,
            Project = model.ProjectId,
            Status = model.Status,
            TaskName = model.TaskName,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static TaskDbModel ToModel(this TaskUpdateInput updateDto, TaskWhereUniqueInput uniqueId)
    {
        var task = new TaskDbModel
        {
            Id = uniqueId.Id,
            Description = updateDto.Description,
            DueDate = updateDto.DueDate,
            Status = updateDto.Status,
            TaskName = updateDto.TaskName
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            task.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            task.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return task;
    }
}
