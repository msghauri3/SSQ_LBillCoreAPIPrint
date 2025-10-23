using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Printing.Models
{
    [Table("MaintenanceBills")]
    public class MaintenanceBill
    {
        [Key]
        public int uid { get; set; }

        [StringLength(50)]
        public string? InvoiceNo { get; set; }

        [StringLength(50)]
        public string? CustomerNo { get; set; }

        [StringLength(100)]
        public string? CustomerName { get; set; }

        [StringLength(50)]
        public string? PlotStatus { get; set; }

        [StringLength(50)]
        public string? MeterNo { get; set; }

        [StringLength(50)]
        public string? BTNo { get; set; }

        [StringLength(50)]
        public string? BillingMonth { get; set; }

        [StringLength(50)]
        public string? BillingYear { get; set; }

        public DateTime? BillingDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ValidDate { get; set; }

        [StringLength(50)]
        public string? PaymentStatus { get; set; }

        public DateTime? PaymentDate { get; set; }

        [StringLength(50)]
        public string? PaymentMethod { get; set; }

        [StringLength(50)]
        public string? BankDetail { get; set; }

        public DateTime? LastUpdated { get; set; }
        public decimal? MaintCharges { get; set; }
        public int? BillAmountInDueDate { get; set; }
        public int? BillSurcharge { get; set; }
        public int? BillAmountAfterDueDate { get; set; }
        public decimal? Arrears { get; set; }
        public string? History { get; set; }
        public int? TaxAmount { get; set; }
        public int? Fine { get; set; }
        public int? OtherCharges { get; set; }
        public int? WaterCharges { get; set; }
    }
}
