using Microsoft.EntityFrameworkCore;
using RealEstateCrm.Infrastructure.Models;

namespace RealEstateCrm.Infrastructure;

public class RealEstateCrmDbContext : DbContext
{
    public RealEstateCrmDbContext(DbContextOptions<RealEstateCrmDbContext> options)
        : base(options) { }

    public DbSet<ClientDbModel> Clients { get; set; }

    public DbSet<PropertyDbModel> Properties { get; set; }

    public DbSet<AgentAssignmentDbModel> AgentAssignments { get; set; }

    public DbSet<AppointmentDbModel> Appointments { get; set; }
}
