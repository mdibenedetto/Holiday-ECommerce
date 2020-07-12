using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dream_holiday.Models
{
    public class Order
    {       

        // ID, Date, Price, Oty, OrderStatus

        public int Id = 0;
        public int nr = 0;
        public DateTime Date;
        public decimal Price = 0;
        public int Qty = 0;
        public bool Status = false;

    }
}
