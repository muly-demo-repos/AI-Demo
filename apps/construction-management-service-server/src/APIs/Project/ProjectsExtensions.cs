using ConstructionManagementService.APIs.Dtos;
using ConstructionManagementService.Infrastructure.Models;

namespace ConstructionManagementService.APIs.Extensions;

public static class ProjectsExtensions
{
    public static Project ToDto(this ProjectDbModel model)
    {
        return new Project
        {
            CreatedAt = model.CreatedAt,
            Description = model.Description,
            EndDate = model.EndDate,
            Id = model.Id,
            ProjectName = model.ProjectName,
            StartDate = model.StartDate,
            Tasks = model.Tasks?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ProjectDbModel ToModel(
        this ProjectUpdateInput updateDto,
        ProjectWhereUniqueInput uniqueId
    )
    {
        var project = new ProjectDbModel
        {
            Id = uniqueId.Id,
            Description = updateDto.Description,
            EndDate = updateDto.EndDate,
            ProjectName = updateDto.ProjectName,
            StartDate = updateDto.StartDate
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            project.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            project.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return project;
    }
}
