using RealEstateCrm.APIs;

namespace RealEstateCrm;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAgentAssignmentsService, AgentAssignmentsService>();
        services.AddScoped<IAppointmentsService, AppointmentsService>();
        services.AddScoped<IClientsService, ClientsService>();
        services.AddScoped<IPropertiesService, PropertiesService>();
    }
}
