using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dream_holiday.Models
{
    [Table("Cart")]
    public class Cart
    {
       
        public Guid Id { get; set; }
        public String Title = "";
        public String Description = "";
        public bool IsInstock = false;
        public int Qty = 0;
        public Decimal Price = 0;
      }
}
