using Net1.APIs.Dtos;
using Net1.Infrastructure.Models;

namespace Net1.APIs.Extensions;

public static class HaimsExtensions
{
    public static Haim ToDto(this HaimDbModel model)
    {
        return new Haim
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static HaimDbModel ToModel(this HaimUpdateInput updateDto, HaimWhereUniqueInput uniqueId)
    {
        var haim = new HaimDbModel { Id = uniqueId.Id };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            haim.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            haim.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return haim;
    }
}
