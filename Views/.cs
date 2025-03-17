using Microsoft.AspNetCore.Mvc;

namespace ARMENIACarShop.Views
{
	public class AccountController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
