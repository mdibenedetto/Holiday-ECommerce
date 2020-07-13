using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dream_holiday.Models
{

    public class OrderDetailItem
    {
        public int nr = 0;
        public String title = " ";
        public String description = " ";
        public int Qty = 0;
        public decimal Price = 0;
    }

    public class OrderDetailsModel
    {

        public long OrderId;
        public DateTime OrderDate;

        public List<OrderDetailItem> Items
            = new List<OrderDetailItem>();
    }
}
