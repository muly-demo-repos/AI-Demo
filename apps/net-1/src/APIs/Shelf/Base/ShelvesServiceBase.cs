using Microsoft.EntityFrameworkCore;
using Net1.APIs;
using Net1.APIs.Common;
using Net1.APIs.Dtos;
using Net1.APIs.Errors;
using Net1.APIs.Extensions;
using Net1.Infrastructure;
using Net1.Infrastructure.Models;

namespace Net1.APIs;

public abstract class ShelvesServiceBase : IShelvesService
{
    protected readonly Net1DbContext _context;

    public ShelvesServiceBase(Net1DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one shelf
    /// </summary>
    public async Task<Shelf> CreateShelf(ShelfCreateInput createDto)
    {
        var shelf = new ShelfDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            shelf.Id = createDto.Id;
        }

        _context.Shelves.Add(shelf);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ShelfDbModel>(shelf.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one shelf
    /// </summary>
    public async Task DeleteShelf(ShelfWhereUniqueInput uniqueId)
    {
        var shelf = await _context.Shelves.FindAsync(uniqueId.Id);
        if (shelf == null)
        {
            throw new NotFoundException();
        }

        _context.Shelves.Remove(shelf);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many shelves
    /// </summary>
    public async Task<List<Shelf>> Shelves(ShelfFindManyArgs findManyArgs)
    {
        var shelves = await _context
            .Shelves.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return shelves.ConvertAll(shelf => shelf.ToDto());
    }

    /// <summary>
    /// Get one shelf
    /// </summary>
    public async Task<Shelf> Shelf(ShelfWhereUniqueInput uniqueId)
    {
        var shelves = await this.shelves(
            new ShelfFindManyArgs { Where = new ShelfWhereInput { Id = uniqueId.Id } }
        );
        var shelf = shelves.FirstOrDefault();
        if (shelf == null)
        {
            throw new NotFoundException();
        }

        return shelf;
    }

    /// <summary>
    /// Meta data about shelf records
    /// </summary>
    public async Task<MetadataDto> ShelvesMeta(ShelfFindManyArgs findManyArgs)
    {
        var count = await _context.Shelves.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update one shelf
    /// </summary>
    public async Task UpdateShelf(ShelfWhereUniqueInput uniqueId, ShelfUpdateInput updateDto)
    {
        var shelf = updateDto.ToModel(uniqueId);

        _context.Entry(shelf).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Shelves.Any(e => e.Id == shelf.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
