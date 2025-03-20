using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ARMENIACarShop.Data;
using ARMENIACarShop.Models;
using ARMENIACarShop.Views.Helpers;
using Microsoft.AspNetCore.Http;

namespace ARMENIACarShop.Controllers
{
    public class CarController : Controller
    {
        private readonly ARMENIACarShopContext _context;

        public CarController(ARMENIACarShopContext context)
        {
            _context = context;
        }

        // GET: Car
        public async Task<IActionResult> Index()
        {
            var carModels = await _context.CarModel.ToListAsync();

            // Ավելացնել sums և counts, որոնք անհրաժեշտ են Index.cshtml-ում
            ViewBag.Sums = new Dictionary<int, decimal>();
            ViewBag.Counts = new Dictionary<int, int>();
            decimal totalSum = 0;
            int totalCount = 0;

            // Ավելացնել տվյալների հաշվարկ
            foreach (var car in carModels)
            {
                var sum = car.Price; // Հաշվում ենք ընդհանուր գինը
                var count = 1; // Ավելացնում ենք հաշվարկի մեկ տարբերակ

                ViewBag.Sums[car.Id] = sum;
                ViewBag.Counts[car.Id] = count;

                totalSum += sum;
                totalCount += count;
            }

            ViewBag.Sum = totalSum;

            return View(carModels);
        }

        // GET: Car/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModel
                .FirstOrDefaultAsync(m => m.Id == id);

            if (carModel == null)
            {
                return NotFound();
            }

            // Բերում ենք տվյալ մեքենայի համար բոլոր կարծիքները
            var reviews = await _context.ReviewModel
                .Where(r => r.Car.Id == id)  // Կապում ենք կարծիքը մեքենայի ID-ի հետ
                .ToListAsync();

            // Ավելացնում ենք դրանք `ViewBag`-ի մեջ
            ViewBag.Reviews = reviews;

            return View(carModel);
        }


        // GET: Car/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Model,Year,Type,Damaged,UnderWater,EngineCapacity,NumberOfDoors,CarMileage,RunAndDrive,Price,Electric")] CarModel carModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carModel);
        }

        // GET: Car/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModel.FindAsync(id);
            if (carModel == null)
            {
                return NotFound();
            }
            return View(carModel);
        }

        // POST: Car/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Model,Year,Type,Damaged,UnderWater,EngineCapacity,NumberOfDoors,CarMileage,RunAndDrive,Price,Electric")] CarModel carModel)
        {
            if (id != carModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarModelExists(carModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(carModel);
        }

        // GET: Car/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carModel = await _context.CarModel.FindAsync(id);
            if (carModel != null)
            {
                _context.CarModel.Remove(carModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarModelExists(int id)
        {
            return _context.CarModel.Any(e => e.Id == id);
        }

        // Գործողություն, որը ավելացնում է մեքենան "My Card"-ում
        public IActionResult AddToMyCard(int id)
        {
            var car = _context.CarModel.FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            // Ստանում ենք "My Card" ցանկը Session-ից
            List<CarModel> myCard = HttpContext.Session.GetObject<List<CarModel>>("MyCard") ?? new List<CarModel>();

            // Ավելացնում ենք մեքենան
            myCard.Add(car);

            // Թարմացնում ենք Session-ը
            HttpContext.Session.SetObject("MyCard", myCard);

            return RedirectToAction("Index");
        }

       
        public IActionResult MyCard()
        {
            var myCard = HttpContext.Session.GetObject<List<CarModel>>("MyCard") ?? new List<CarModel>();
            return View(myCard);
        }


       





    }

}
