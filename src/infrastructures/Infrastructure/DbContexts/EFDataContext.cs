using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.DbContexts;
public class EFDataContext : DbContext
{

    public EFDataContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  
    }
}



