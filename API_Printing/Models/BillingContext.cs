using Microsoft.EntityFrameworkCore;

namespace API_Printing.Models
{
    public partial class BillingContext : DbContext
    {
        public BillingContext(DbContextOptions<BillingContext> options)
            : base(options)
        {
        }

        
        public DbSet<ElectricityBill> EBills { get; set; }
        public DbSet<MaintenanceBill> MaintenanceBills { get; set; }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
