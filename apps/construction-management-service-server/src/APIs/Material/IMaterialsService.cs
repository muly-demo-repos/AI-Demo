using ConstructionManagementService.APIs.Common;
using ConstructionManagementService.APIs.Dtos;

namespace ConstructionManagementService.APIs;

public interface IMaterialsService
{
    /// <summary>
    /// Create one Material
    /// </summary>
    public Task<Material> CreateMaterial(MaterialCreateInput material);

    /// <summary>
    /// Delete one Material
    /// </summary>
    public Task DeleteMaterial(MaterialWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Materials
    /// </summary>
    public Task<List<Material>> Materials(MaterialFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Material
    /// </summary>
    public Task<Material> Material(MaterialWhereUniqueInput uniqueId);

    /// <summary>
    /// Meta data about Material records
    /// </summary>
    public Task<MetadataDto> MaterialsMeta(MaterialFindManyArgs findManyArgs);

    /// <summary>
    /// Update one Material
    /// </summary>
    public Task UpdateMaterial(MaterialWhereUniqueInput uniqueId, MaterialUpdateInput updateDto);
}
