using ARMENIACarShop.Data;
using ARMENIACarShop.Models;
using ARMENIACarShop.Views.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ARMENIACarShop.Views.Home.OrderSession
{
    public class OrderSessionController : Controller
    {
        private readonly ARMENIACarShopContext _context;
        private readonly string SessionKey = "OrderSession";

        public OrderSessionController(ARMENIACarShopContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
			var order = HttpContext.Session.GetObject<List<CarModel>>(SessionKey) ?? new List<CarModel>();

			Dictionary<int, int> counts = new Dictionary<int, int>();
			Dictionary<int, decimal> sums = new Dictionary<int, decimal>();
			List<CarModel> uniqueCars = new List<CarModel>();

			foreach (var o in order)
			{
				if (counts.ContainsKey(o.Id))
				{
					counts[o.Id] += 1;
					sums[o.Id] += o.Price;
				}
				else
				{
					counts.Add(o.Id, 1);
					sums.Add(o.Id, o.Price);
					uniqueCars.Add(o);
				}

			}

			ViewBag.Counts = counts;
			ViewBag.Sums = sums;
			ViewBag.Sum = sums.Values.Sum();
            return View(uniqueCars);
		}



        public IActionResult AddToOrder(int id)
        {
            var car = _context.CarModel.Find(id);
            if (car == null) return NotFound();

            var order = HttpContext.Session.GetObject<List<CarModel>>(SessionKey) ?? new List<CarModel>();
            order.Add(car);

            HttpContext.Session.SetObject(SessionKey, order);
            return RedirectToAction("Index");
        }



        public IActionResult RemoveFromOrder(int id)
        {
            var order = HttpContext.Session.GetObject<List<CarModel>>(SessionKey) ?? new List<CarModel>();
            var carToRemove = order.FirstOrDefault(b => b.Id == id);

            if (carToRemove != null)
            {
                order.Remove(carToRemove);
                HttpContext.Session.SetObject(SessionKey, order);

            }
            return RedirectToAction("Index");

        }



        public IActionResult ClearOrder()
        {
            HttpContext.Session.Remove(SessionKey);
            return RedirectToAction("Index");
        }


    }
}

