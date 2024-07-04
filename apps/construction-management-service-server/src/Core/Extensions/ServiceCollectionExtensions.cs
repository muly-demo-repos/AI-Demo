using ConstructionManagementService.APIs;

namespace ConstructionManagementService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IMaterialsService, MaterialsService>();
        services.AddScoped<IProjectsService, ProjectsService>();
        services.AddScoped<ITasksService, TasksService>();
        services.AddScoped<IWorkersService, WorkersService>();
    }
}
