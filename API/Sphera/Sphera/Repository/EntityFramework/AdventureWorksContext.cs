using Microsoft.EntityFrameworkCore;
using Sphera.Repository.EntityFramework.DbModels;
using System.Diagnostics;

namespace Sphera.Repository.EntityFramework
{
    public class AdventureWorksContext : DbContext
    {
        public AdventureWorksContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
        public DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesOrderHeader>()
                .HasKey(n => n.SalesOrderId);

            modelBuilder.Entity<SalesOrderDetail>()
                .HasKey(n => new { n.SalesOrderId, n.SalesOrderDetailId });

            modelBuilder.Entity<Contact>()
                .HasKey(n => n.ContactId);

            modelBuilder.Entity<SalesOrderHeader>()
              .Property(n => n.SalesOrderId).HasColumnName("SalesOrderID");


            modelBuilder.Entity<SalesOrderHeader>()
              .Property(n => n.ContactId).HasColumnName("ContactID");

            modelBuilder.Entity<SalesOrderDetail>()
                .Property(n => n.SalesOrderId).HasColumnName("SalesOrderID");

            modelBuilder.Entity<Contact>()
                .Property(n => n.ContactId).HasColumnName("ContactID");

            modelBuilder.Entity<SalesOrderDetail>()
                .Property(n => n.SalesOrderDetailId).HasColumnName("SalesOrderDetailID");

            modelBuilder.Entity<SalesOrderHeader>()
                .HasMany(n => n.SalesOrderDetails)
                .WithOne(n => n.SalesOrderHeader)
                .HasForeignKey(n => n.SalesOrderId)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .HasMany(n => n.SalesOrderHeaders)
                .WithOne(n => n.Contact)
                .HasForeignKey(n => n.ContactId)
                .IsRequired();


            base.OnModelCreating(modelBuilder);
        }
    }
}
