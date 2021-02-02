using Microsoft.EntityFrameworkCore;
using CommonStore.Core.Data;
using CommonStore.Core.Messages;
using CommonStore.Sales.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CommonStore.Sales.Data
{
    public class SalesContext : DbContext, IUnitOfWork
    {
        //DbContextOptions will have some configuration on StartUp
        public SalesContext(DbContextOptions<SalesContext> options)
            : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet <Voucher> Vouchers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //All varchar columns will have 100 as max lenght instead of MAX, on db creation time
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetMaxLength(100);

            modelBuilder.Ignore<Event>();
            
            //Search for all entities and its mappings configured on the project
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesContext).Assembly);

            modelBuilder.HasSequence<int>("MinhaSequencia").StartsAt(1000).IncrementsBy(1);
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            //ChangeTracker - EntityFramework's tracking mapper
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreateDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreateDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreateDate").IsModified = false;
                }
            }

            //More than 1 row updated on DB
            return await base.SaveChangesAsync() > 0;
        }
    }
}
