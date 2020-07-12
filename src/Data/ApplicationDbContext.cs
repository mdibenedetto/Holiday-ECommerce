using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using dream_holiday.Models;
using Microsoft.AspNetCore.Identity;

namespace dream_holiday.Data
{
    public class ApplicationDbContext :
         IdentityDbContext<ApplicationUser, ApplicationRole, Guid> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserAccount> UserAccount{ get; set; }

        public DbSet<Cart> Cart { get; set; } 

        public DbSet<Checkout> Checkout { get; set; }

    }
}
