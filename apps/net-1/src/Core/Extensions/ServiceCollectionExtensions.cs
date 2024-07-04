using Net1.APIs;

namespace Net1;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IHaimsService, HaimsService>();
        services.AddScoped<IUsService, UsService>();
        services.AddScoped<IMorsService, MorsService>();
        services.AddScoped<IMuliesService, MuliesService>();
        services.AddScoped<IShelvesService, ShelvesService>();
        services.AddScoped<IWomenService, WomenService>();
    }
}
