using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Net1.Infrastructure.Models;

namespace Net1.Infrastructure;

public class Net1DbContext : IdentityDbContext<IdentityUser>
{
    public Net1DbContext(DbContextOptions<Net1DbContext> options)
        : base(options) { }

    public DbSet<MulyDbModel> Mulies { get; set; }

    public DbSet<MorDbModel> Mors { get; set; }

    public DbSet<HaimDbModel> Haims { get; set; }

    public DbSet<WomanDbModel> Women { get; set; }

    public DbSet<ShelfDbModel> Shelves { get; set; }

    public DbSet<MeDbModel> Us { get; set; }
}
