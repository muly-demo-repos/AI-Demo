using Net1.APIs.Common;
using Net1.APIs.Dtos;

namespace Net1.APIs;

public interface IShelvesService
{
    /// <summary>
    /// Create one shelf
    /// </summary>
    public Task<Shelf> CreateShelf(ShelfCreateInput shelf);

    /// <summary>
    /// Delete one shelf
    /// </summary>
    public Task DeleteShelf(ShelfWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many shelves
    /// </summary>
    public Task<List<Shelf>> Shelves(ShelfFindManyArgs findManyArgs);

    /// <summary>
    /// Get one shelf
    /// </summary>
    public Task<Shelf> Shelf(ShelfWhereUniqueInput uniqueId);

    /// <summary>
    /// Meta data about shelf records
    /// </summary>
    public Task<MetadataDto> ShelvesMeta(ShelfFindManyArgs findManyArgs);

    /// <summary>
    /// Update one shelf
    /// </summary>
    public Task UpdateShelf(ShelfWhereUniqueInput uniqueId, ShelfUpdateInput updateDto);
}
