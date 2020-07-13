using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dream_holiday.Models
{
    [Table("Cart")]
    public class Cart
    {       
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public bool IsInstock { get; set; }
        public int Qty { get; set; }
        public Decimal Price { get; set; }
    }
}
