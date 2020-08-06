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

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<UserAccount> UserAccount { get; set; }

        public DbSet<Cart> Cart { get; set; }

        public DbSet<Checkout> Checkout { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<OrderDetail> OrderDetail { get; set; }

        public DbSet<TravelPackage> TravelPackage { get; set; }

        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderDetail>()
                .HasKey(c => new { c.Id, c.OrderId }); 

             //modelBuilder
             //  .Entity<Category>()
               
             //  .HasOne(a => a.TravelPackage)
             //  .WithOne(b => b.Category)
             //  .IsRequired(false)
             //  .HasForeignKey<TravelPackage>(t=> t.CategoryId)
             //  .IsRequired(false) ;

            //modelBuilder.Entity<Blog>()
            //         .HasOne(b => b.BlogImage)
            //         .WithOne(i => i.Blog)
            //         .HasForeignKey<BlogImage>(b => b.BlogForeignKey);
        }
    }

    //public class Blog
    //{
    //    public int BlogId { get; set; }
    //    public string Url { get; set; }

    //    public BlogImage BlogImage { get; set; }
    //}

    //public class BlogImage
    //{
    //    public int BlogImageId { get; set; }
    //    public byte[] Image { get; set; }
    //    public string Caption { get; set; }

    //    public int BlogForeignKey { get; set; }
    //    public Blog Blog { get; set; }
    //}
}
