using ConstructionManagementService.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConstructionManagementService.Infrastructure;

public class ConstructionManagementServiceDbContext : IdentityDbContext<IdentityUser>
{
    public ConstructionManagementServiceDbContext(
        DbContextOptions<ConstructionManagementServiceDbContext> options
    )
        : base(options) { }

    public DbSet<ProjectDbModel> Projects { get; set; }

    public DbSet<WorkerDbModel> Workers { get; set; }

    public DbSet<TaskDbModel> Tasks { get; set; }

    public DbSet<MaterialDbModel> Materials { get; set; }
}
