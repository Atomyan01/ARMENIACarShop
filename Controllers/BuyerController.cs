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
    public class BuyerController : Controller
    {
        private readonly ARMENIACarShopContext _context;

        public BuyerController(ARMENIACarShopContext context)
        {
            _context = context;
        }

        // GET: Buyer
        public async Task<IActionResult> Index()
        {
            return View(await _context.BuyerModel.ToListAsync());
        }

        // GET: Buyer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyerModel = await _context.BuyerModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (buyerModel == null)
            {
                return NotFound();
            }

            return View(buyerModel);
        }

        // GET: Buyer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buyer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirsName,LastName,Email,Password,PhoneNumber")] BuyerModel buyerModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buyerModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

        // POST: Buyer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirsName,LastName,Email,Password,PhoneNumber")] BuyerModel buyerModel)
        {
            if (id != buyerModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buyerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuyerModelExists(buyerModel.Id))
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
            return View(buyerModel);
        }

        // GET: Buyer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyerModel = await _context.BuyerModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (buyerModel == null)
            {
                return NotFound();
            }

            return View(buyerModel);
        }

        // POST: Buyer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buyerModel = await _context.BuyerModel.FindAsync(id);
            if (buyerModel != null)
            {
                _context.BuyerModel.Remove(buyerModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuyerModelExists(int id)
        {
            return _context.BuyerModel.Any(e => e.Id == id);
        }
    }
}
