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
    public class SellersBackEndPageController : Controller
    {
        private readonly OllieShopContext _context;

        public SellersBackEndPageController(OllieShopContext context)
        {
            _context = context;
        }

        // GET: SellersBackEndPage/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var sellers = await _context.Sellers
                .Include(s => s.UR)
                .FirstOrDefaultAsync(m => m.SRID == id);
            if (sellers == null)
            {
                return NotFound();
            }

            return View(sellers);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var sellers = await _context.Sellers.FindAsync(id);
            if (sellers == null)
            {
                return NotFound();
            }
            ViewData["URID"] = new SelectList(_context.Users, "URID", "Email", sellers.URID);
            return View(sellers);
        }

        // POST: SellersBackEndPage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("SRID,ShopNAME,TaxID,BankCode,BankAccount,URID")] Sellers sellers)
        {
            if (id != sellers.SRID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sellers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellersExists(sellers.SRID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["editSuccessMessage"] = "本次修改成功! 您可再次確認是否符合預期";
                return RedirectToAction(nameof(Details), new { id = sellers.SRID });
            }

            return View(sellers);
        }


        private bool SellersExists(long id)
        {
          return (_context.Sellers?.Any(e => e.SRID == id)).GetValueOrDefault();
        }
    }
}
