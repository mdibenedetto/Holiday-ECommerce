using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dream_holiday.Models
{
    public class Category
    {
        public TravelPackage TravelPackage { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Int32 Id { get; set; }

        [Required]
        public String Code { get; set; }

        public String Description { get; set; }

    }
}
