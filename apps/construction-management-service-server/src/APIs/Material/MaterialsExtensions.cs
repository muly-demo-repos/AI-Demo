using ConstructionManagementService.APIs.Dtos;
using ConstructionManagementService.Infrastructure.Models;

namespace ConstructionManagementService.APIs.Extensions;

public static class MaterialsExtensions
{
    public static Material ToDto(this MaterialDbModel model)
    {
        return new Material
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            MaterialName = model.MaterialName,
            Quantity = model.Quantity,
            Unit = model.Unit,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static MaterialDbModel ToModel(
        this MaterialUpdateInput updateDto,
        MaterialWhereUniqueInput uniqueId
    )
    {
        var material = new MaterialDbModel
        {
            Id = uniqueId.Id,
            MaterialName = updateDto.MaterialName,
            Quantity = updateDto.Quantity,
            Unit = updateDto.Unit
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            material.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            material.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return material;
    }
}
