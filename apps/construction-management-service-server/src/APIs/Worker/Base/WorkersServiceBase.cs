using ConstructionManagementService.APIs;
using ConstructionManagementService.APIs.Common;
using ConstructionManagementService.APIs.Dtos;
using ConstructionManagementService.APIs.Errors;
using ConstructionManagementService.APIs.Extensions;
using ConstructionManagementService.Infrastructure;
using ConstructionManagementService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructionManagementService.APIs;

public abstract class WorkersServiceBase : IWorkersService
{
    protected readonly ConstructionManagementServiceDbContext _context;

    public WorkersServiceBase(ConstructionManagementServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Worker
    /// </summary>
    public async Task<Worker> CreateWorker(WorkerCreateInput createDto)
    {
        var worker = new WorkerDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Role = createDto.Role,
            UpdatedAt = createDto.UpdatedAt,
            WorkerName = createDto.WorkerName
        };

        if (createDto.Id != null)
        {
            worker.Id = createDto.Id;
        }

        _context.Workers.Add(worker);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<WorkerDbModel>(worker.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Worker
    /// </summary>
    public async Task DeleteWorker(WorkerWhereUniqueInput uniqueId)
    {
        var worker = await _context.Workers.FindAsync(uniqueId.Id);
        if (worker == null)
        {
            throw new NotFoundException();
        }

        _context.Workers.Remove(worker);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Workers
    /// </summary>
    public async Task<List<Worker>> Workers(WorkerFindManyArgs findManyArgs)
    {
        var workers = await _context
            .Workers.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return workers.ConvertAll(worker => worker.ToDto());
    }

    /// <summary>
    /// Get one Worker
    /// </summary>
    public async Task<Worker> Worker(WorkerWhereUniqueInput uniqueId)
    {
        var workers = await this.Workers(
            new WorkerFindManyArgs { Where = new WorkerWhereInput { Id = uniqueId.Id } }
        );
        var worker = workers.FirstOrDefault();
        if (worker == null)
        {
            throw new NotFoundException();
        }

        return worker;
    }

    /// <summary>
    /// Update one Worker
    /// </summary>
    public async Task UpdateWorker(WorkerWhereUniqueInput uniqueId, WorkerUpdateInput updateDto)
    {
        var worker = updateDto.ToModel(uniqueId);

        _context.Entry(worker).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Workers.Any(e => e.Id == worker.Id))
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
    /// Meta data about Worker records
    /// </summary>
    public async Task<MetadataDto> WorkersMeta(WorkerFindManyArgs findManyArgs)
    {
        var count = await _context.Workers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }
}
