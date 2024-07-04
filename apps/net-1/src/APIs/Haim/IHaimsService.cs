using Net1.APIs.Common;
using Net1.APIs.Dtos;

namespace Net1.APIs;

public interface IHaimsService
{
    /// <summary>
    /// Create one Haims
    /// </summary>
    public Task<Haim> CreateHaim(HaimCreateInput haim);

    /// <summary>
    /// Delete one Haims
    /// </summary>
    public Task DeleteHaim(HaimWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Haims
    /// </summary>
    public Task<List<Haim>> Haims(HaimFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Haims
    /// </summary>
    public Task<Haim> Haim(HaimWhereUniqueInput uniqueId);

    /// <summary>
    /// Meta data about Haims records
    /// </summary>
    public Task<MetadataDto> HaimsMeta(HaimFindManyArgs findManyArgs);

    /// <summary>
    /// Update one Haims
    /// </summary>
    public Task UpdateHaim(HaimWhereUniqueInput uniqueId, HaimUpdateInput updateDto);
}
