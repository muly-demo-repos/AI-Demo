using Net1.APIs.Dtos;
using Net1.Infrastructure.Models;

namespace Net1.APIs.Extensions;

public static class MuliesExtensions
{
    public static Muly ToDto(this MulyDbModel model)
    {
        return new Muly
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static MulyDbModel ToModel(this MulyUpdateInput updateDto, MulyWhereUniqueInput uniqueId)
    {
        var muly = new MulyDbModel { Id = uniqueId.Id };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            muly.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            muly.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return muly;
    }
}
