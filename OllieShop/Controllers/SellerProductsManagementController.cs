using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OllieShop.Models;
using OllieShop.ViewModels;

namespace OllieShop.Controllers
{
    public class SellerProductsManagementController : Controller
    {
        private readonly OllieShopContext _context;

        public SellerProductsManagementController(OllieShopContext context)
        {
            _context = context;
        }

        // GET: SellerProductsManagement
        public async Task<IActionResult> Index()
        {

            VMSellerProductsManagement productPlusSpec = new VMSellerProductsManagement() {
                productTable = _context.Products.ToList(),
                specificationsTable = _context.Specifications.ToList(),
            };
            
            //return View(await ollieShopContext.ToListAsync());
            return View(productPlusSpec);
        }

        // GET: SellerProductsManagement/Details/5
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

        // GET: SellerProductsManagement/Create
        public IActionResult Create()
        {
            ViewData["CYID"] = new SelectList(_context.Categorys, "CYID", "CYID");
            ViewData["SRID"] = new SelectList(_context.Sellers, "SRID", "BankAccount");
            return View();
        }

        // POST: SellerProductsManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PTID,Name,DeliveryFee,LaunchDate,Hidden,Locked,Inquired,Installment,Unopened,UnitPrice,ShelfQuantity,SoldQuantity,Description,CYID,SRID")] Products products)
        {
            if (ModelState.IsValid)
            {
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CYID"] = new SelectList(_context.Categorys, "CYID", "CYID", products.CYID);
            ViewData["SRID"] = new SelectList(_context.Sellers, "SRID", "BankAccount", products.SRID);
            return View(products);
        }

        // GET: SellerProductsManagement/Edit/5
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
            ViewData["CYID"] = new SelectList(_context.Categorys, "CYID", "CYID", products.CYID);
            ViewData["SRID"] = new SelectList(_context.Sellers, "SRID", "BankAccount", products.SRID);
            return View(products);
        }

        // POST: SellerProductsManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewData["CYID"] = new SelectList(_context.Categorys, "CYID", "CYID", products.CYID);
            ViewData["SRID"] = new SelectList(_context.Sellers, "SRID", "BankAccount", products.SRID);
            return View(products);
        }

        // GET: SellerProductsManagement/Delete/5
        public async Task<IActionResult> Delete(long? id)
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

        // POST: SellerProductsManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'OllieShopContext.Products'  is null.");
            }
            var products = await _context.Products.FindAsync(id);
            if (products != null)
            {
                _context.Products.Remove(products);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(long id)
        {
          return (_context.Products?.Any(e => e.PTID == id)).GetValueOrDefault();
        }
    }
}
