using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dream_holiday.Models
{
    [Table("Cart")]
    public class Cart
    {
        public virtual TravelPackage TravelPackage { get; set; }
        public virtual UserAccount UserAccount { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public int Qty { get; set; }
        public Decimal Price { get; set; }
    }
}
