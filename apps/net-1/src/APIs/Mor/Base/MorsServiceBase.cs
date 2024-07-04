using Microsoft.EntityFrameworkCore;
using Net1.APIs;
using Net1.APIs.Common;
using Net1.APIs.Dtos;
using Net1.APIs.Errors;
using Net1.APIs.Extensions;
using Net1.Infrastructure;
using Net1.Infrastructure.Models;

namespace Net1.APIs;

public abstract class MorsServiceBase : IMorsService
{
    protected readonly Net1DbContext _context;

    public MorsServiceBase(Net1DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one mor. $% ?
    /// </summary>
    public async Task<Mor> CreateMor(MorCreateInput createDto)
    {
        var mor = new MorDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            mor.Id = createDto.Id;
        }

        _context.Mors.Add(mor);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<MorDbModel>(mor.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one mor. $% ?
    /// </summary>
    public async Task DeleteMor(MorWhereUniqueInput uniqueId)
    {
        var mor = await _context.Mors.FindAsync(uniqueId.Id);
        if (mor == null)
        {
            throw new NotFoundException();
        }

        _context.Mors.Remove(mor);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many mors
    /// </summary>
    public async Task<List<Mor>> Mors(MorFindManyArgs findManyArgs)
    {
        var mors = await _context
            .Mors.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return mors.ConvertAll(mor => mor.ToDto());
    }

    /// <summary>
    /// Get one mor. $% ?
    /// </summary>
    public async Task<Mor> Mor(MorWhereUniqueInput uniqueId)
    {
        var mors = await this.mors(
            new MorFindManyArgs { Where = new MorWhereInput { Id = uniqueId.Id } }
        );
        var mor = mors.FirstOrDefault();
        if (mor == null)
        {
            throw new NotFoundException();
        }

        return mor;
    }

    /// <summary>
    /// Meta data about mor. $% ? records
    /// </summary>
    public async Task<MetadataDto> MorsMeta(MorFindManyArgs findManyArgs)
    {
        var count = await _context.Mors.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update one mor. $% ?
    /// </summary>
    public async Task UpdateMor(MorWhereUniqueInput uniqueId, MorUpdateInput updateDto)
    {
        var mor = updateDto.ToModel(uniqueId);

        _context.Entry(mor).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Mors.Any(e => e.Id == mor.Id))
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
