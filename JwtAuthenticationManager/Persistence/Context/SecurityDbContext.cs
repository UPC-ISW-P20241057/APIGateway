using JwtAuthenticationManager.Domain.Models;
using JwtAuthenticationManager.Extensions;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthenticationManager.Persistence.Context;

public class SecurityDbContext(DbContextOptions options): DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p => p.Id);
        builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(p => p.Email).IsRequired();
        builder.Entity<User>().Property(p => p.Role).IsRequired();
        
        // Apply Snake Case Naming Convention
        
        builder.UseSnakeCaseNamingConvention();
    }
}