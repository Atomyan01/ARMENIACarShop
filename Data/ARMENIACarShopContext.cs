using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ARMENIACarShop.Models;

namespace ARMENIACarShop.Data
{
    public class ARMENIACarShopContext : DbContext
    {
        public ARMENIACarShopContext (DbContextOptions<ARMENIACarShopContext> options)
            : base(options)
        {
        }

        public DbSet<ARMENIACarShop.Models.PersonModel> PersonModel { get; set; } = default!;
        public DbSet<ARMENIACarShop.Models.BuyerModel> BuyerModel { get; set; } = default!;
        public DbSet<ARMENIACarShop.Models.CarModel> CarModel { get; set; } = default!;
        public DbSet<ARMENIACarShop.Models.SellerModel> SellerModel { get; set; } = default!;
    }
}
