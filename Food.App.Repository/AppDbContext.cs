using Food.App.Core.Entities;
using Food.App.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Food.App.Repository;

public class AppDbContext : DbContext
{
    public DbSet<Person> Person { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RoleFeature> RoleFeatures { get; set; }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<RecipeTag> RecipeTags { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        //optionsBuilder.LogTo(Console.WriteLine).EnableSensitiveDataLogging();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>(entity =>
        {
            entity.UseTptMappingStrategy();

            entity.Property(e => e.Role)
            .HasConversion(r => r.ToString(), r => (Role)Enum.Parse(typeof(Role), r));

            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Phone).IsUnique();
        });
    }
}
