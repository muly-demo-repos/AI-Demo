using RealEstateCrm.APIs.Dtos;
using RealEstateCrm.Infrastructure.Models;

namespace RealEstateCrm.APIs.Extensions;

public static class PropertiesExtensions
{
    public static Property ToDto(this PropertyDbModel model)
    {
        return new Property
        {
            Address = model.Address,
            AgentAssignments = model.AgentAssignments?.Select(x => x.Id).ToList(),
            CreatedAt = model.CreatedAt,
            Description = model.Description,
            Id = model.Id,
            Price = model.Price,
            Size = model.Size,
            Status = model.Status,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PropertyDbModel ToModel(
        this PropertyUpdateInput updateDto,
        PropertyWhereUniqueInput uniqueId
    )
    {
        var property = new PropertyDbModel
        {
            Id = uniqueId.Id,
            Address = updateDto.Address,
            Description = updateDto.Description,
            Price = updateDto.Price,
            Size = updateDto.Size,
            Status = updateDto.Status
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            property.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            property.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return property;
    }
}
