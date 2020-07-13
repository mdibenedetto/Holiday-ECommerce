using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dream_holiday.Models
{
    public class Order
    {
        public virtual UserAccount Customer { get; set; }
        public virtual List<OrderDetail> Details { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public bool Status { get; set; }

    }
}
