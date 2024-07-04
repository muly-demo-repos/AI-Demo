using Net1.APIs.Common;
using Net1.APIs.Dtos;

namespace Net1.APIs;

public interface IWomenService
{
    /// <summary>
    /// Create one Woman
    /// </summary>
    public Task<Woman> CreateWoman(WomanCreateInput woman);

    /// <summary>
    /// Delete one Woman
    /// </summary>
    public Task DeleteWoman(WomanWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Women
    /// </summary>
    public Task<List<Woman>> Women(WomanFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Woman
    /// </summary>
    public Task<Woman> Woman(WomanWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Woman
    /// </summary>
    public Task UpdateWoman(WomanWhereUniqueInput uniqueId, WomanUpdateInput updateDto);

    /// <summary>
    /// Meta data about Woman records
    /// </summary>
    public Task<MetadataDto> WomenMeta(WomanFindManyArgs findManyArgs);
}
