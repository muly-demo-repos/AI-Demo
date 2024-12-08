using Net1.APIs.Dtos;
using Net1.Infrastructure.Models;

namespace Net1.APIs.Extensions;

public static class UsExtensions
{
    public static Me ToDto(this MeDbModel model)
    {
        return new Me
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static MeDbModel ToModel(this MeUpdateInput updateDto, MeWhereUniqueInput uniqueId)
    {
        var me = new MeDbModel { Id = uniqueId.Id };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            me.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            me.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return me;
    }
}
