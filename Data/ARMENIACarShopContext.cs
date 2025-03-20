using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ARMENIACarShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ARMENIACarShop.Data
{
    public class ARMENIACarShopContext : IdentityDbContext<BuyerModel>  // Ընդհանուր IdentityUser մոդելը դարձնում ենք PersonModel
    {
        public ARMENIACarShopContext(DbContextOptions<ARMENIACarShopContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ավելացնում ենք BuyerModel- ի մասին կարգավորումը
            modelBuilder.Entity<BuyerModel>().ToTable("Buyer");

            // Այստեղ դուք կարող եք ավելացնել այլ կոնֆիգուրացիաներ
        }

        // DbSet-երը
        public DbSet<BuyerModel> BuyerModel { get; set; }
        public DbSet<CarModel> CarModel { get; set; }
        public DbSet<SellerModel> SellerModel { get; set; }

        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<ReviewModel> ReviewModel { get; set; }

    }
}