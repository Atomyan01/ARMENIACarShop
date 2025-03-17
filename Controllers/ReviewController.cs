using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ARMENIACarShop.Data;
using ARMENIACarShop.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ARMENIACarShop.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ARMENIACarShopContext _context;
        private readonly UserManager<BuyerModel> _userManager;

        public ReviewController(ARMENIACarShopContext context, UserManager<BuyerModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult AddReview(int? id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([Bind("Content,Stars,Description")] ReviewModel reviewModel, int carId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            BuyerModel? user = await _context.BuyerModel.FindAsync(userId);
            var car = await _context.CarModel.FindAsync(carId);

            if (user == null) return Unauthorized();
            if (car == null) return NotFound();

            if (reviewModel.Stars < 0)
            {
                reviewModel.Stars = 0;
            }
            else if (reviewModel.Stars > 10)
            {
                reviewModel.Stars = 10;
            }

            ReviewModel review = new ReviewModel
            {
                Content = reviewModel.Content,
                Description = reviewModel.Description,
                Stars = reviewModel.Stars,
                Buyer = user,
                Car = car
            };

            _context.ReviewModel.Add(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Car", new { id = carId });
        }
    }
}
