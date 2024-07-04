using Microsoft.EntityFrameworkCore;
using Net1.APIs;
using Net1.APIs.Common;
using Net1.APIs.Dtos;
using Net1.APIs.Errors;
using Net1.APIs.Extensions;
using Net1.Infrastructure;
using Net1.Infrastructure.Models;

namespace Net1.APIs;

public abstract class HaimsServiceBase : IHaimsService
{
    protected readonly Net1DbContext _context;

    public HaimsServiceBase(Net1DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Haims
    /// </summary>
    public async Task<Haim> CreateHaim(HaimCreateInput createDto)
    {
        var haim = new HaimDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            haim.Id = createDto.Id;
        }

        _context.Haims.Add(haim);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<HaimDbModel>(haim.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Haims
    /// </summary>
    public async Task DeleteHaim(HaimWhereUniqueInput uniqueId)
    {
        var haim = await _context.Haims.FindAsync(uniqueId.Id);
        if (haim == null)
        {
            throw new NotFoundException();
        }

        _context.Haims.Remove(haim);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Haims
    /// </summary>
    public async Task<List<Haim>> Haims(HaimFindManyArgs findManyArgs)
    {
        var haims = await _context
            .Haims.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return haims.ConvertAll(haim => haim.ToDto());
    }

    /// <summary>
    /// Get one Haims
    /// </summary>
    public async Task<Haim> Haim(HaimWhereUniqueInput uniqueId)
    {
        var haims = await this.Haims(
            new HaimFindManyArgs { Where = new HaimWhereInput { Id = uniqueId.Id } }
        );
        var haim = haims.FirstOrDefault();
        if (haim == null)
        {
            throw new NotFoundException();
        }

        return haim;
    }

    /// <summary>
    /// Meta data about Haims records
    /// </summary>
    public async Task<MetadataDto> HaimsMeta(HaimFindManyArgs findManyArgs)
    {
        var count = await _context.Haims.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update one Haims
    /// </summary>
    public async Task UpdateHaim(HaimWhereUniqueInput uniqueId, HaimUpdateInput updateDto)
    {
        var haim = updateDto.ToModel(uniqueId);

        _context.Entry(haim).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Haims.Any(e => e.Id == haim.Id))
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
