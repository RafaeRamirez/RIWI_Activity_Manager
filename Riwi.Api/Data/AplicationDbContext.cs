using Microsoft.EntityFrameworkCore;
using Riwi.Api.Models;

namespace Riwi.Api.Data;

public class AplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Access> Access { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Access>()
            .ToTable("access")
            .HasKey(a => a.id_access);

        modelBuilder.Entity<Access>()
            .Property(a => a.id_access)
            .ValueGeneratedOnAdd();
    }
}