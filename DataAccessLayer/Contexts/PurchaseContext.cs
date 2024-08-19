using DataAccessLayer.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contexts
{
    public class PurchaseContext : DbContext
    {
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<Item> Items { get; set; }

        IConfiguration configuration;

        public PurchaseContext(DbContextOptions<PurchaseContext> options,IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = configuration.GetConnectionString("PurchaseConnection");
            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var Purchases = modelBuilder.Entity<Purchase>();

            Purchases.HasKey(p => p.id).IsClustered();
            Purchases.Property(p => p.name).IsRequired();

            var PurchaseItems = modelBuilder.Entity<PurchaseItem>();

            PurchaseItems.HasKey(p => p.id).IsClustered();

            var Items = modelBuilder.Entity<Item>();
            Items.HasKey(p => p.id).IsClustered();


            base.OnModelCreating(modelBuilder);
        }
    }
}
