﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace dream_holiday.Models
{
    public class TravelPackage
    {
        public virtual Int32 CategoryId { get; set; }
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public String Country { get; set; }

        public String City { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public int Qty { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.##}")]
        public decimal Price { get; set; }

        public String Image { get; set; }

        public bool IsInstock { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public TravelPackage()
        {
            IsInstock = true; 
        }

    }
}
