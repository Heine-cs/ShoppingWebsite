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
    public class PaymentMethodsController : Controller
    {
        private readonly OllieShopContext _context;

        public PaymentMethodsController(OllieShopContext context)
        {
            _context = context;
        }

        // GET: PaymentMethods
        public async Task<IActionResult> Index()
        {
              return _context.PaymentMethods != null ? 
                          View(await _context.PaymentMethods.ToListAsync()) :
                          Problem("Entity set 'OllieShopContext.PaymentMethods'  is null.");
        }

        // GET: PaymentMethods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentMethods/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PMID,Name")] PaymentMethods paymentMethods)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentMethods);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentMethods);
        }

        // GET: PaymentMethods/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.PaymentMethods == null)
            {
                return NotFound();
            }

            var paymentMethods = await _context.PaymentMethods
                .FirstOrDefaultAsync(m => m.PMID == id);
            if (paymentMethods == null)
            {
                return NotFound();
            }

            return View(paymentMethods);
        }

        // POST: PaymentMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.PaymentMethods == null)
            {
                return Problem("Entity set 'OllieShopContext.PaymentMethods'  is null.");
            }
            var paymentMethods = await _context.PaymentMethods.FindAsync(id);
            if (paymentMethods != null)
            {
                _context.PaymentMethods.Remove(paymentMethods);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentMethodsExists(string id)
        {
          return (_context.PaymentMethods?.Any(e => e.PMID == id)).GetValueOrDefault();
        }
    }
}
