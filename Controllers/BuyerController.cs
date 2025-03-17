using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ARMENIACarShop.Data;
using ARMENIACarShop.Models;
using Microsoft.AspNetCore.Identity;

namespace ARMENIACarShop.Controllers
{
	public class BuyerController : Controller
	{
		private readonly ARMENIACarShopContext _context;
		private readonly UserManager<BuyerModel> _userManager;
		private readonly SignInManager<BuyerModel> _signInManager;

		public BuyerController(ARMENIACarShopContext context, SignInManager<BuyerModel> signInManager, UserManager<BuyerModel> userManager)
		{
			_context = context;
			_signInManager = signInManager;
			_userManager = userManager;
		}

		// GET: Buyer
		public async Task<IActionResult> Index()
		{
			return View(await _context.BuyerModel.ToListAsync());
		}

		// GET: Buyer/Create
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Age,FirstName,LastName,Email,Password")] BuyerModel buyerModel, string password)
		{
			// Set lockout properties and normalize the email
			buyerModel.LockoutEnabled = false;
			buyerModel.NormalizedEmail = _userManager.NormalizeEmail(buyerModel.Email);
			buyerModel.NormalizedUserName = buyerModel.Email;
			buyerModel.PasswordHash = password;
			ModelState.Remove("PhoneNumber");

			if (ModelState.IsValid)
			{
				// Create a new user and set the properties
				var user = new BuyerModel
				{
					UserName = buyerModel.Email,
					Email = buyerModel.Email,
					FirstName = buyerModel.FirstName,
					LastName = buyerModel.LastName,
					Age = buyerModel.Age,
					NormalizedEmail = buyerModel.NormalizedEmail,
					PasswordHash = buyerModel.PasswordHash,
					Id = Guid.NewGuid().ToString() // Add the ID here if needed
				};

				// Create the user in the system
				var result = await _userManager.CreateAsync(user, password);

				if (result.Succeeded)
				{
					// If successful, sign in the user
					await _signInManager.SignInAsync(user, isPersistent: false);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					// If there are errors, add them to the model state
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}
			}

			return View(buyerModel);
		}

		// GET: Buyer/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var buyerModel = await _context.BuyerModel.FindAsync(id);
			if (buyerModel == null)
			{
				return NotFound();
			}
			return View(buyerModel);
		}

		// Login
		[HttpPost]
		public async Task<IActionResult> Login(string email, string password)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user != null)
			{
				var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Invalid Email or Password");
				}
			}
			else
			{
				ModelState.AddModelError("", "User Not Found");
			}
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}

		private BuyerModel? SearchByEmail(string email)
		{
			return _context.BuyerModel.FirstOrDefault(user => user.Email == email);
		}

	}
}
