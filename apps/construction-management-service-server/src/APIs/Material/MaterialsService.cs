using ConstructionManagementService.Infrastructure;

namespace ConstructionManagementService.APIs;

public class MaterialsService : MaterialsServiceBase
{
    public MaterialsService(ConstructionManagementServiceDbContext context)
        : base(context) { }
}
