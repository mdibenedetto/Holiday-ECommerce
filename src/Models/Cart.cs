using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dream_holiday.Models
{
    [Table("Cart")]
    public class Cart
    {
        public virtual TravelPackage TravelPackage { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        // todo: remove 
        [Obsolete("remove - use TravelPackage.Name")]
        [NotMapped]
        public String Title { get; set; }
        // todo: remove
        [Obsolete("remove - use TravelPackage.Description")]
        [NotMapped]
        public String Description { get; set; }
        // todo: remove
        [Obsolete("remove - use TravelPackage.IsInstock")]
        [NotMapped]
        public bool IsInstock { get; set; }

        public int Qty { get; set; }

        public Decimal Price { get; set; }
    }
}
