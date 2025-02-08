using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ARMENIACarShop.Data;
using ARMENIACarShop.Models;

namespace ARMENIACarShop.Controllers
{
    public class SellerController : Controller
    {
        private readonly ARMENIACarShopContext _context;

        public SellerController(ARMENIACarShopContext context)
        {
            _context = context;
        }

        // GET: Seller
        public async Task<IActionResult> Index()
        {
            return View(await _context.SellerModel.ToListAsync());
        }

        // GET: Seller/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sellerModel = await _context.SellerModel
                .FirstOrDefaultAsync(m => m.Id == id);
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvarageScore,Id,FirsName,LastName,Email,Password,PhoneNumber")] SellerModel sellerModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sellerModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sellerModel);
        }

        // GET: Seller/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AvarageScore,Id,FirsName,LastName,Email,Password,PhoneNumber")] SellerModel sellerModel)
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sellerModel = await _context.SellerModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sellerModel == null)
            {
                return NotFound();
            }

            return View(sellerModel);
        }

        // POST: Seller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sellerModel = await _context.SellerModel.FindAsync(id);
            if (sellerModel != null)
            {
                _context.SellerModel.Remove(sellerModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellerModelExists(int id)
        {
            return _context.SellerModel.Any(e => e.Id == id);
        }
    }
}
