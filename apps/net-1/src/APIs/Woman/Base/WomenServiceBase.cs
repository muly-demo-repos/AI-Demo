using Microsoft.EntityFrameworkCore;
using Net1.APIs;
using Net1.APIs.Common;
using Net1.APIs.Dtos;
using Net1.APIs.Errors;
using Net1.APIs.Extensions;
using Net1.Infrastructure;
using Net1.Infrastructure.Models;

namespace Net1.APIs;

public abstract class WomenServiceBase : IWomenService
{
    protected readonly Net1DbContext _context;

    public WomenServiceBase(Net1DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Woman
    /// </summary>
    public async Task<Woman> CreateWoman(WomanCreateInput createDto)
    {
        var woman = new WomanDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            woman.Id = createDto.Id;
        }

        _context.Women.Add(woman);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<WomanDbModel>(woman.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Woman
    /// </summary>
    public async Task DeleteWoman(WomanWhereUniqueInput uniqueId)
    {
        var woman = await _context.Women.FindAsync(uniqueId.Id);
        if (woman == null)
        {
            throw new NotFoundException();
        }

        _context.Women.Remove(woman);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Women
    /// </summary>
    public async Task<List<Woman>> Women(WomanFindManyArgs findManyArgs)
    {
        var women = await _context
            .Women.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return women.ConvertAll(woman => woman.ToDto());
    }

    /// <summary>
    /// Get one Woman
    /// </summary>
    public async Task<Woman> Woman(WomanWhereUniqueInput uniqueId)
    {
        var women = await this.Women(
            new WomanFindManyArgs { Where = new WomanWhereInput { Id = uniqueId.Id } }
        );
        var woman = women.FirstOrDefault();
        if (woman == null)
        {
            throw new NotFoundException();
        }

        return woman;
    }

    /// <summary>
    /// Update one Woman
    /// </summary>
    public async Task UpdateWoman(WomanWhereUniqueInput uniqueId, WomanUpdateInput updateDto)
    {
        var woman = updateDto.ToModel(uniqueId);

        _context.Entry(woman).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Women.Any(e => e.Id == woman.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Meta data about Woman records
    /// </summary>
    public async Task<MetadataDto> WomenMeta(WomanFindManyArgs findManyArgs)
    {
        var count = await _context.Women.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }
}
