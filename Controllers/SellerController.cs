using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ARMENIACarShop.Data;
using ARMENIACarShop.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;

namespace ARMENIACarShop.Controllers
{
    public class SellerController : Controller
    {
        private readonly ARMENIACarShopContext _context;
        private readonly SignInManager<SellerModel> _signInManager;
        private readonly UserManager<SellerModel> _userManager;

        public SellerController(ARMENIACarShopContext context, SignInManager<SellerModel> signInManager, UserManager<SellerModel> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: Seller
        public async Task<IActionResult> Index()
        {
            return View(await _context.SellerModel.ToListAsync());
        }

        // GET: Seller/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sellerModel = await _context.SellerModel.FirstOrDefaultAsync(m => m.Id == id);
            if (sellerModel == null)
            {
                return NotFound();
            }

            return View(sellerModel);
        }

        // GET: Seller/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Seller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Age,Address,FirstName,LastName,Email")] SellerModel userModel, string password)
        {
            if (ModelState.IsValid)
            {
                userModel.UserName = userModel.Email;
                userModel.NormalizedEmail = _userManager.NormalizeEmail(userModel.Email);
                userModel.NormalizedUserName = userModel.Email;
                userModel.LockoutEnabled = false;

                var result = await _userManager.CreateAsync(userModel, password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(userModel, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(userModel);
        }

        // GET: Seller/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sellerModel = await _context.SellerModel.FindAsync(id);
            if (sellerModel == null)
            {
                return NotFound();
            }
            return View(sellerModel);
        }

        // POST: Seller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,Email,PhoneNumber")] SellerModel sellerModel)
        {
            if (id != sellerModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sellerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerModelExists(sellerModel.Id))
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
            return View(sellerModel);
        }

        // GET: Seller/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sellerModel = await _context.SellerModel.FirstOrDefaultAsync(m => m.Id == id);
            if (sellerModel == null)
            {
                return NotFound();
            }

            return View(sellerModel);
        }

        // POST: Seller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sellerModel = await _context.SellerModel.FindAsync(id);
            if (sellerModel != null)
            {
                _context.SellerModel.Remove(sellerModel);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SellerModelExists(string id)
        {
            return _context.SellerModel.Any(e => e.Id == id);
        }

        private bool CheckEmail(string email)
        {
            var authors = _context.SellerModel.ToListAsync().Result;

            foreach (SellerModel author in authors)
            {
                if (author.Email == email)
                {
                    return true;
                }
            }

            string emailPattern = "^\\S+@\\S+\\.\\S+$";
            Regex regex = new Regex(emailPattern);
            return !regex.IsMatch(email);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(SellerModel seller)
        {
            var user = SearchByEmail(seller.Email);
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction(nameof(Index));
        }

        private SellerModel? SearchByEmail(string email)
        {
            return _context.SellerModel.FirstOrDefault(user => user.Email == email);
        }
    }
}