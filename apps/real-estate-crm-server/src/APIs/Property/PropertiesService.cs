using RealEstateCrm.Infrastructure;

namespace RealEstateCrm.APIs;

public class PropertiesService : PropertiesServiceBase
{
    public PropertiesService(RealEstateCrmDbContext context)
        : base(context) { }
}
