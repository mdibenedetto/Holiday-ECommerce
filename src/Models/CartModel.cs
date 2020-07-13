using System;
namespace dream_holiday.Models
{
    public class CartModel
    {
        public CartModel()
        {
        }

        public String Title = "";
        public String Description = "";
        public bool IsInstock = false;
        public int Qty = 0;
        public Decimal price = 0;
      }
}
