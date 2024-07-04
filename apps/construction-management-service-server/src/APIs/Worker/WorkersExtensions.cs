using ConstructionManagementService.APIs.Dtos;
using ConstructionManagementService.Infrastructure.Models;

namespace ConstructionManagementService.APIs.Extensions;

public static class WorkersExtensions
{
    public static Worker ToDto(this WorkerDbModel model)
    {
        return new Worker
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Role = model.Role,
            UpdatedAt = model.UpdatedAt,
            WorkerName = model.WorkerName,
        };
    }

    public static WorkerDbModel ToModel(
        this WorkerUpdateInput updateDto,
        WorkerWhereUniqueInput uniqueId
    )
    {
        var worker = new WorkerDbModel
        {
            Id = uniqueId.Id,
            Role = updateDto.Role,
            WorkerName = updateDto.WorkerName
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            worker.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            worker.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return worker;
    }
}
