using ARMENIACarShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ARMENIACarShop.Controllers
{
	public class AccountController : Controller
	{

		private readonly SignInManager<BuyerModel> _signInManager;

		public AccountController(SignInManager<BuyerModel> signInManager)
		{
			_signInManager = signInManager;
		}

		public IActionResult Login()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		public IActionResult Logout()
		{
			return View();
		}

		

		public async Task<IActionResult> LogoutConfirmed()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
