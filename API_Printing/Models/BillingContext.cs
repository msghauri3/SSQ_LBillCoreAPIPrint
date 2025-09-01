using Microsoft.EntityFrameworkCore;

namespace API_Printing.Models
{
    public partial class BillingContext : DbContext
    {
        public BillingContext(DbContextOptions<BillingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Configurations> Configurations { get; set; }
        public  DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Configurations>(entity =>
            {
                entity.HasKey(e => e.Uid);
                entity.ToTable("Configuration");

                entity.Property(e => e.ConfigKey)
                      .HasMaxLength(50);

                entity.Property(e => e.ConfigValue)
                      .HasMaxLength(100);
            });
            
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
