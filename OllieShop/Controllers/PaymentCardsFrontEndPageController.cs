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
    public class PaymentCardsFrontEndPageController : Controller
    {
        private readonly OllieShopContext _context;

        public PaymentCardsFrontEndPageController(OllieShopContext context)
        {
            _context = context;
        }

        // GET: PaymentCardsFrontEndPage/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.PaymentCards == null)
            {
                return NotFound();
            }

            var paymentCards = await _context.PaymentCards
                .Include(p => p.CR)
                .FirstOrDefaultAsync(m => m.PCID == id);
            if (paymentCards == null)
            {
                return NotFound();
            }

            return View(paymentCards);
        }

        // GET: PaymentCardsFrontEndPage/Create
        public IActionResult Create()
        {
            ViewData["CRID"] = new SelectList(_context.Customers, "CRID", "CRID");
            return View();
        }

        // POST: PaymentCardsFrontEndPage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PCID,ExpiryDate,SecurityCode,Number,BillAddress,CRID")] PaymentCards paymentCards)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentCards);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = paymentCards.PCID});
            }
            ViewData["CRID"] = new SelectList(_context.Customers, "CRID", "CRID", paymentCards.CRID);
            return View(paymentCards);
        }

        // GET: PaymentCardsFrontEndPage/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.PaymentCards == null)
            {
                return NotFound();
            }

            var paymentCards = await _context.PaymentCards.FindAsync(id);
            if (paymentCards == null)
            {
                return NotFound();
            }
            ViewData["CRID"] = new SelectList(_context.Customers, "CRID", "CRID", paymentCards.CRID);
            return View(paymentCards);
        }

        // POST: PaymentCardsFrontEndPage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("PCID,ExpiryDate,SecurityCode,Number,BillAddress,CRID")] PaymentCards paymentCards)
        {
            if (id != paymentCards.PCID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentCards);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentCardsExists(paymentCards.PCID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = paymentCards.PCID });
            }
            ViewData["CRID"] = new SelectList(_context.Customers, "CRID", "CRID", paymentCards.CRID);
            return View(paymentCards);
        }

        // GET: PaymentCardsFrontEndPage/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.PaymentCards == null)
            {
                return NotFound();
            }

            var paymentCards = await _context.PaymentCards
                .Include(p => p.CR)
                .FirstOrDefaultAsync(m => m.PCID == id);
            if (paymentCards == null)
            {
                return NotFound();
            }

            return View(paymentCards);
        }

        // POST: PaymentCardsFrontEndPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.PaymentCards == null)
            {
                return Problem("Entity set 'OllieShopContext.PaymentCards'  is null.");
            }
            var paymentCards = await _context.PaymentCards.FindAsync(id);
            if (paymentCards != null)
            {
                _context.PaymentCards.Remove(paymentCards);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentCardsExists(short id)
        {
          return (_context.PaymentCards?.Any(e => e.PCID == id)).GetValueOrDefault();
        }
    }
}
