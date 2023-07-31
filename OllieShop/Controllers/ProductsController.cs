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
    public class ProductsController : Controller
    {
        private readonly OllieShopContext _context;

        public ProductsController(OllieShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ollieShopContext = _context.Products.Include(p => p.CY).Include(p => p.SR);
            return View(await ollieShopContext.ToListAsync());
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.CY)
                .Include(p => p.SR)
                .FirstOrDefaultAsync(m => m.PTID == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("PTID,Name,DeliveryFee,LaunchDate,Hidden,Locked,Inquired,Installment,Unopened,UnitPrice,ShelfQuantity,SoldQuantity,Description,CYID,SRID")] Products products)
        {
            if (id != products.PTID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.PTID))
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

            return View(products);
        }

        private bool ProductsExists(long id)
        {
          return (_context.Products?.Any(e => e.PTID == id)).GetValueOrDefault();
        }
    }
}
