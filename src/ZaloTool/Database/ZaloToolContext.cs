```csharp
using Microsoft.EntityFrameworkCore;
using src.ZaloTool.Models;
using MmoTool.ZaloTool.Services;

namespace MmoTool.ZaloTool.Database;

public class ZaloToolContext : DbContext
{
    public ZaloToolContext(DbContextOptions<ZaloToolContext> options) : base(options)
    {
    }

    public DbSet<AccountZalo> AccountZalos { get; set; }
    public DbSet<PhoneZalo> PhoneZalos { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<SettingSendMessagePhoneNumber> SettingSendMessagePhoneNumbers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Add configurations here if needed
        modelBuilder.Entity<AccountZalo>().HasKey(a => a.Id);
        modelBuilder.Entity<PhoneZalo>().HasKey(p => p.Id);
    }
}