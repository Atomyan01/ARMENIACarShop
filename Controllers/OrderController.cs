using ARMENIACarShop.Data;
using ARMENIACarShop.Models;
using ARMENIACarShop.Views.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARMENIACarShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ARMENIACarShopContext _context;
        private readonly UserManager<BuyerModel> _userManager;
        private const string SessionKey = "OrderSession";

        public OrderController(ARMENIACarShopContext context, UserManager<BuyerModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PlaceOrder()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var orderCars = HttpContext.Session.GetObject<List<CarModel>>(SessionKey) ?? new List<CarModel>();

            // Ստուգենք, որ կա մեքենաների ցանկ
            if (!orderCars.Any())
                return BadRequest("Your order is empty");

            decimal totalPrice = orderCars.Sum(car => car.Price);
            int totalCars = orderCars.Count;

            OrderModel newOrder = new OrderModel
            {
                UserId = user.Id,  // Այստեղ պետք է լինի `UserId`, քանի որ `OrderModel`-ը չունի `User`
                Cars = orderCars,
                TotalPrice = totalPrice,
                TotalCars = totalCars,
                Status = "Pending",
                Date = DateTime.Now
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            // Վերացնում ենք սեսիայի տվյալները
            HttpContext.Session.Remove(SessionKey);

            return RedirectToAction("MyOrders");
        }


        public async Task<IActionResult> MyOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            // Ստեղծենք list-ը, որը կարող է լինել դատարկ
            var orders = await _context.Orders
                .Where(o => o.UserId == user.Id)  // Այստեղ մենք համադրում ենք UserId-ի հետ, ինչը string է
                .OrderByDescending(o => o.Date)
                .ToListAsync();

            // Ստուգենք, որ արդյունքները կան
            if (!orders.Any())
            {
                ViewBag.ErrorMessage = "No orders found.";
                return View(new List<OrderModel>());
            }

            return View(orders);
        }

    }
}
