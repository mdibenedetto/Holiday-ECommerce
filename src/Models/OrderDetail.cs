using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dream_holiday.Models
{   
    public class OrderDetail
    {         
        public virtual TravelPackage TravelPackage { get; set; }
        public virtual Order Order { get; set; }

        [Key()]
        public int OrderId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key()]
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }        
    }
}
