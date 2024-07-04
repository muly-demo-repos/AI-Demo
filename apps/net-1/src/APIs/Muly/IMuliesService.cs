using Net1.APIs.Common;
using Net1.APIs.Dtos;

namespace Net1.APIs;

public interface IMuliesService
{
    /// <summary>
    /// Create one Muly
    /// </summary>
    public Task<Muly> CreateMuly(MulyCreateInput muly);

    /// <summary>
    /// Delete one Muly
    /// </summary>
    public Task DeleteMuly(MulyWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Mulies
    /// </summary>
    public Task<List<Muly>> Mulies(MulyFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Muly
    /// </summary>
    public Task<Muly> Muly(MulyWhereUniqueInput uniqueId);

    /// <summary>
    /// Meta data about Muly records
    /// </summary>
    public Task<MetadataDto> MuliesMeta(MulyFindManyArgs findManyArgs);

    /// <summary>
    /// Update one Muly
    /// </summary>
    public Task UpdateMuly(MulyWhereUniqueInput uniqueId, MulyUpdateInput updateDto);
}
