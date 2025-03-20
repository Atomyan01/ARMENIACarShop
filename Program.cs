using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ARMENIACarShop.Data;
using ARMENIACarShop.Models;
using Microsoft.AspNetCore.Identity;
namespace ARMENIACarShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ARMENIACarShopContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ARMENIACarShopContext") ?? throw new InvalidOperationException("Connection string 'ARMENIACarShopContext' not found.")));

            builder.Services.AddIdentity<BuyerModel, IdentityRole>().
            AddEntityFrameworkStores<ARMENIACarShopContext>().
            AddDefaultTokenProviders();

            // Add services to the container.
            builder.Services.AddControllersWithViews();



            builder.Services.AddSession();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();


            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}