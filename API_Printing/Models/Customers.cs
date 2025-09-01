using System;

namespace API_Printing.Models
{
    public class Customer
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public string? postal_code { get; set; }
        public int? loyalty_points { get; set; }
        public decimal? total_spent { get; set; }
        public bool is_active { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
