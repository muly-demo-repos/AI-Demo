using ConstructionManagementService.APIs.Common;
using ConstructionManagementService.APIs.Dtos;

namespace ConstructionManagementService.APIs;

public interface IWorkersService
{
    /// <summary>
    /// Create one Worker
    /// </summary>
    public Task<Worker> CreateWorker(WorkerCreateInput worker);

    /// <summary>
    /// Delete one Worker
    /// </summary>
    public Task DeleteWorker(WorkerWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Workers
    /// </summary>
    public Task<List<Worker>> Workers(WorkerFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Worker
    /// </summary>
    public Task<Worker> Worker(WorkerWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Worker
    /// </summary>
    public Task UpdateWorker(WorkerWhereUniqueInput uniqueId, WorkerUpdateInput updateDto);

    /// <summary>
    /// Meta data about Worker records
    /// </summary>
    public Task<MetadataDto> WorkersMeta(WorkerFindManyArgs findManyArgs);
}
