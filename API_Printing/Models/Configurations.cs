using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Printing.Models
{
    public class Configurations
    {
        [Key]
        [Column("UID")]
        public int Uid { get; set; }

        [StringLength(50)]
        public string? ConfigKey { get; set; }

        [StringLength(50)]
        public string? ConfigValue { get; set; }
    }

}