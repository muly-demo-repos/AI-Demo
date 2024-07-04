using Net1.APIs.Common;
using Net1.APIs.Dtos;

namespace Net1.APIs;

public interface IUsService
{
    /// <summary>
    /// Create one us
    /// </summary>
    public Task<Me> CreateMe(MeCreateInput me);

    /// <summary>
    /// Delete one us
    /// </summary>
    public Task DeleteMe(MeWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many us
    /// </summary>
    public Task<List<Me>> Us(MeFindManyArgs findManyArgs);

    /// <summary>
    /// Get one us
    /// </summary>
    public Task<Me> Me(MeWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one us
    /// </summary>
    public Task UpdateMe(MeWhereUniqueInput uniqueId, MeUpdateInput updateDto);

    /// <summary>
    /// Meta data about us records
    /// </summary>
    public Task<MetadataDto> UsMeta(MeFindManyArgs findManyArgs);
}
