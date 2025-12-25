using Microsoft.EntityFrameworkCore;

namespace WorkService.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<WorkItem> WorkItems => Set<WorkItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WorkItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Status).HasMaxLength(50).IsRequired();
        });
    }
}

public class WorkItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Status { get; set; } = "PENDING";
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
