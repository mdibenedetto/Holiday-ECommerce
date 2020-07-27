using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dream_holiday.Models
{
    [Table("Checkout")]
    public class Checkout
    {
        public virtual UserAccount UserAccount { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public String FirstName { get; set; }
        public String LastName = "";
        public String UserName = "";
        public String Email = "";

        public String Address = "";
        public String Address2 = "";

        public String Country = "";
        public String City = "";
        public String EirCode = "";

        public String PaymentMethod = "";

        public String NameOnCard = "";

        public String CardNumber = "";
        public DateTime Expiration;
        public String CVC = "";

        // todo: remove this attributes
        public String FirstItem = "";
        public String SecondItem = "";
        public String ThirdItem = "";

        public String GetUserName()
        {
            return FirstName + LastName;
        }

    }
}
