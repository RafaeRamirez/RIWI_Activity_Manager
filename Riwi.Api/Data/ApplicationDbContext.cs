using Microsoft.EntityFrameworkCore;
using Riwi.Api.Models;

namespace Riwi.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Riwi.Api.Models.Program> Programs { get; set; }
    public DbSet<Cohort> Cohorts { get; set; }
    public DbSet<CoderProfile> CoderProfiles { get; set; }
    public DbSet<Location> Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>()
            .HasIndex(p => p.Email)
            .IsUnique();
    }
}