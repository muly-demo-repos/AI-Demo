using Net1.APIs.Common;
using Net1.APIs.Dtos;

namespace Net1.APIs;

public interface IMorsService
{
    /// <summary>
    /// Create one mor. $% ?
    /// </summary>
    public Task<Mor> CreateMor(MorCreateInput mor);

    /// <summary>
    /// Delete one mor. $% ?
    /// </summary>
    public Task DeleteMor(MorWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many mors
    /// </summary>
    public Task<List<Mor>> Mors(MorFindManyArgs findManyArgs);

    /// <summary>
    /// Get one mor. $% ?
    /// </summary>
    public Task<Mor> Mor(MorWhereUniqueInput uniqueId);

    /// <summary>
    /// Meta data about mor. $% ? records
    /// </summary>
    public Task<MetadataDto> MorsMeta(MorFindManyArgs findManyArgs);

    /// <summary>
    /// Update one mor. $% ?
    /// </summary>
    public Task UpdateMor(MorWhereUniqueInput uniqueId, MorUpdateInput updateDto);
}
