using Net1.APIs.Dtos;
using Net1.Infrastructure.Models;

namespace Net1.APIs.Extensions;

public static class ShelvesExtensions
{
    public static Shelf ToDto(this ShelfDbModel model)
    {
        return new Shelf
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ShelfDbModel ToModel(
        this ShelfUpdateInput updateDto,
        ShelfWhereUniqueInput uniqueId
    )
    {
        var shelf = new ShelfDbModel { Id = uniqueId.Id };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            shelf.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            shelf.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return shelf;
    }
}
