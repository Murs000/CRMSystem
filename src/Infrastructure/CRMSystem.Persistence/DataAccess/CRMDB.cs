using CRMSystem.Domain.Entities;
using CRMSystem.Persistence.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.Persistence.DataAccess;

public class CRMDB : DbContext
{
    public CRMDB(DbContextOptions<CRMDB> options) : base(options)
    {
    }

    // DbSets for the entities
    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply configurations for each entity
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}