using Net1.APIs.Dtos;
using Net1.Infrastructure.Models;

namespace Net1.APIs.Extensions;

public static class WomenExtensions
{
    public static Woman ToDto(this WomanDbModel model)
    {
        return new Woman
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static WomanDbModel ToModel(
        this WomanUpdateInput updateDto,
        WomanWhereUniqueInput uniqueId
    )
    {
        var woman = new WomanDbModel { Id = uniqueId.Id };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            woman.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            woman.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return woman;
    }
}
