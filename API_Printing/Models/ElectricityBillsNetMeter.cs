using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Printing.Models
{
    [Table("ElectricityBillsNetMeter")]
    public class ElectricityBillsNetMeter
    {
        [Key]
        public int uid { get; set; }

        public string? InvoiceNo { get; set; }
        public string? CustomerNo { get; set; }
        public string? CustomerName { get; set; }
        public string? BTNo { get; set; }
        public string? BillingMonth { get; set; }
        public string? BillingYear { get; set; }
        public DateTime? BillingDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ReadingDate { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ValidDate { get; set; }
        public string? MeterType { get; set; }
        public string? MeterNo { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? BankDetail { get; set; }
        public int? EnergyCoast { get; set; }
        public decimal? CurrentBill { get; set; }
        public decimal? BillAmount { get; set; }
        public string? LastUpdated { get; set; }
        public int? BillAmountInDueDate { get; set; }
        public int? BillSurcharge { get; set; }
        public int? BillAmountAfterDueDate { get; set; }
        public int? PreviousReading1 { get; set; }
        public int? CurrentReading1 { get; set; }
        public int? Difference1 { get; set; }
        public int? PreviousReading2 { get; set; }
        public int? CurrentReading2 { get; set; }
        public int? Difference2 { get; set; }
        public int? PreviousSolarReading { get; set; }
        public int? CurrentSolarReading { get; set; }
        public int? DifferenceSolar { get; set; }
        public decimal? TotalUnit { get; set; }
        public decimal? OPC { get; set; }
        public decimal? GST { get; set; }
        public decimal? PTVFEE { get; set; }
        public decimal? FURTHERTAX { get; set; }
        public decimal? FPACHARGES { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreateOn { get; set; }
        public string? UpdateBy { get; set; }
        public string? UpdateOn { get; set; }
        public decimal? Arrears { get; set; }
        public string? History { get; set; }
        public int? AmountPaid { get; set; }
        public string? Sector { get; set; }
        public string? Block { get; set; }
        public DateTime? PushDate { get; set; }
        public string? PushedBy { get; set; }
        public string? InstalledOn { get; set; }
        public decimal? OPCRate { get; set; }
        public int? UnitsAmount { get; set; }
        public decimal? fpa1 { get; set; }
        public decimal? fpa2 { get; set; }
        public decimal? fpa3 { get; set; }
        public string? FPARate { get; set; }
        public string? MeterStatus { get; set; }
        public int? Fine { get; set; }
        public int? SalesTax { get; set; }
        public int? IncomeTax { get; set; }
        public int? ExtraTax { get; set; }
        public int? SumUnitsImport { get; set; }
        public int? SumUnitsExport { get; set; }
        public decimal? SumRateImport { get; set; }
        public decimal? SumRateExport { get; set; }
        public int? SumAmountImport { get; set; }
        public int? SumAmountExport { get; set; }
        public int? NMPreviousCredit { get; set; }
        public int? NMCurrentCredit { get; set; }
        public int? NMTotalCredit { get; set; }
    }
}
