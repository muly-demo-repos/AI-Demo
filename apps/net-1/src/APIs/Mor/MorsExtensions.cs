using Net1.APIs.Dtos;
using Net1.Infrastructure.Models;

namespace Net1.APIs.Extensions;

public static class MorsExtensions
{
    public static Mor ToDto(this MorDbModel model)
    {
        return new Mor
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static MorDbModel ToModel(this MorUpdateInput updateDto, MorWhereUniqueInput uniqueId)
    {
        var mor = new MorDbModel { Id = uniqueId.Id };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            mor.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            mor.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return mor;
    }
}
