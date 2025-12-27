using Microsoft.EntityFrameworkCore;
using WorkService.Api.Data;
using WorkService.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Connection string: from env var (Container Apps secret -> env var)
var cs = builder.Configuration.GetConnectionString("DefaultConnection")
         ?? builder.Configuration["ConnectionStrings__DefaultConnection"]
         ?? builder.Configuration["DefaultConnection"];

if (string.IsNullOrWhiteSpace(cs))
{
    // For local dev fallback (optional)
    cs = builder.Configuration.GetConnectionString("LocalConnection");
}

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(cs));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// OPTIONAL: health endpoint for Container Apps
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

// Simple CRUD endpoints (minimal API)
app.MapGet("/api/work-items", async (AppDbContext db) =>
    await db.WorkItems.OrderByDescending(x => x.Id).ToListAsync());

app.MapPost("/api/work-items", async (AppDbContext db, WorkItem item) =>
{
    db.WorkItems.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/api/work-items/{item.Id}", item);
});

app.MapPatch("/api/work-items/{id:int}/done", async (AppDbContext db, int id) =>
{
    var item = await db.WorkItems.FindAsync(id);
    if (item is null) return Results.NotFound();
    item.IsDone = true;
    await db.SaveChangesAsync();
    return Results.Ok(item);
});

// Run EF migrations at startup (simple approach)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

app.Run();
