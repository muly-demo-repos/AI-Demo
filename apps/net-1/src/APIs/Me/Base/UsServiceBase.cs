using Microsoft.EntityFrameworkCore;
using Net1.APIs;
using Net1.APIs.Common;
using Net1.APIs.Dtos;
using Net1.APIs.Errors;
using Net1.APIs.Extensions;
using Net1.Infrastructure;
using Net1.Infrastructure.Models;

namespace Net1.APIs;

public abstract class UsServiceBase : IUsService
{
    protected readonly Net1DbContext _context;

    public UsServiceBase(Net1DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one us
    /// </summary>
    public async Task<Me> CreateMe(MeCreateInput createDto)
    {
        var me = new MeDbModel { CreatedAt = createDto.CreatedAt, UpdatedAt = createDto.UpdatedAt };

        if (createDto.Id != null)
        {
            me.Id = createDto.Id;
        }

        _context.Us.Add(me);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<MeDbModel>(me.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one us
    /// </summary>
    public async Task DeleteMe(MeWhereUniqueInput uniqueId)
    {
        var me = await _context.Us.FindAsync(uniqueId.Id);
        if (me == null)
        {
            throw new NotFoundException();
        }

        _context.Us.Remove(me);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many us
    /// </summary>
    public async Task<List<Me>> Us(MeFindManyArgs findManyArgs)
    {
        var us = await _context
            .Us.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return us.ConvertAll(me => me.ToDto());
    }

    /// <summary>
    /// Get one us
    /// </summary>
    public async Task<Me> Me(MeWhereUniqueInput uniqueId)
    {
        var us = await this.us(
            new MeFindManyArgs { Where = new MeWhereInput { Id = uniqueId.Id } }
        );
        var me = us.FirstOrDefault();
        if (me == null)
        {
            throw new NotFoundException();
        }

        return me;
    }

    /// <summary>
    /// Update one us
    /// </summary>
    public async Task UpdateMe(MeWhereUniqueInput uniqueId, MeUpdateInput updateDto)
    {
        var me = updateDto.ToModel(uniqueId);

        _context.Entry(me).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Us.Any(e => e.Id == me.Id))
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
    /// Meta data about us records
    /// </summary>
    public async Task<MetadataDto> UsMeta(MeFindManyArgs findManyArgs)
    {
        var count = await _context.Us.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }
}
