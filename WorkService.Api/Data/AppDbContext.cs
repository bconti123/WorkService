using Microsoft.EntityFrameworkCore;
using WorkService.Api.Models;

namespace WorkService.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<WorkItem> WorkItems => Set<WorkItem>();
}
