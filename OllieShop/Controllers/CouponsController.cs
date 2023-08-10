using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OllieShop.Models;

namespace OllieShop.Controllers
{
    public class CouponsController : Controller
    {
        private readonly OllieShopContext _context;

        public CouponsController(OllieShopContext context)
        {
            _context = context;
        }

        // GET: Coupons
        public async Task<IActionResult> Index()
        {
              return _context.Coupons != null ? 
                          View(await _context.Coupons.ToListAsync()) :
                          Problem("Entity set 'OllieShopContext.Coupons'  is null.");
        }

        // GET: Coupons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Coupons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CNID,CODE,ExpiryDate,Discount")] Coupons coupons)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coupons);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coupons);
        }

        // GET: Coupons/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Coupons == null)
            {
                return NotFound();
            }

            var coupons = await _context.Coupons.FindAsync(id);
            if (coupons == null)
            {
                return NotFound();
            }
            return View(coupons);
        }

        // POST: Coupons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("CNID,CODE,ExpiryDate,Discount")] Coupons coupons)
        {
            if (id != coupons.CNID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coupons);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CouponsExists(coupons.CNID))
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
            return View(coupons);
        }

        // GET: Coupons/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Coupons == null)
            {
                return NotFound();
            }

            var coupons = await _context.Coupons
                .FirstOrDefaultAsync(m => m.CNID == id);
            if (coupons == null)
            {
                return NotFound();
            }

            return View(coupons);
        }

        // POST: Coupons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Coupons == null)
            {
                return Problem("Entity set 'OllieShopContext.Coupons'  is null.");
            }
            var coupons = await _context.Coupons.FindAsync(id);
            if (coupons != null)
            {
                _context.Coupons.Remove(coupons);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CouponsExists(long id)
        {
          return (_context.Coupons?.Any(e => e.CNID == id)).GetValueOrDefault();
        }
    }
}
