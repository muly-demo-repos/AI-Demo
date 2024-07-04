using Microsoft.EntityFrameworkCore;

namespace FurnitureService.Infrastructure;

public class FurnitureServiceDbContext : DbContext
{
    public FurnitureServiceDbContext(DbContextOptions<FurnitureServiceDbContext> options)
        : base(options) { }
}
