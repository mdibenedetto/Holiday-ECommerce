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

        [Required]
        public String LastName { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        public String Address { get; set; }
        public String Address2 { get; set; }

        [Required]
        public String Country { get; set; }

        [Required]
        public String City { get; set; }

        [Required]
        public String EirCode { get; set; }

        [Required]
        public String PaymentMethod { get; set; }

        [Required(ErrorMessage = "The name of the card holder is required")]
        public String NameOnCard { get; set; }

        [Required (ErrorMessage = "A valid credit card is required")]
        [CreditCard( ErrorMessage = "Credit card is not valid (ex. 4111 1111 1111 1111)")]
        public String CardNumber { get; set; }

        [Required(ErrorMessage= "The credit card expiration date is required")]
        public DateTime Expiration { get; set; }

        [Required (ErrorMessage="The CVC is required")]
        [MaxLength(4, ErrorMessage = "The CVC as to be a number with 3 or 4 digits")]
        [RegularExpression(@"^[0-9]{3,4}$",
            ErrorMessage = "Security code required and has to be a number with 3 digits or 4 digits (ex 123 or 1234)")]
        public String CVC { get; set; }
    }
}
