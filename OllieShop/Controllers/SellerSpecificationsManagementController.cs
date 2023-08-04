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
    public class SellerSpecificationsManagementController : Controller
    {
        private readonly OllieShopContext _context;

        public SellerSpecificationsManagementController(OllieShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ollieShopContext = _context.Specifications.Include(s => s.PT);
            return View(await ollieShopContext.ToListAsync());
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Specifications == null)
            {
                return NotFound();
            }

            var specifications = await _context.Specifications
                .Include(s => s.PT)
                .FirstOrDefaultAsync(m => m.SNID == id);
            if (specifications == null)
            {
                return NotFound();
            }

            return View(specifications);
        }

        public IActionResult Create()
        {
            ViewData["SRID"] = "2";

            ViewData["PTID"] = new SelectList(_context.Products, "PTID", "Description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SNID,SpecName,Picture,Weight,Size,LeadDay,PackageSize,Freebie,PTID")] Specifications specifications)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specifications);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PTID"] = new SelectList(_context.Products, "PTID", "Description", specifications.PTID);
            return View(specifications);
        }

        // GET: SellerSpecificationsManagerment/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Specifications == null)
            {
                return NotFound();
            }

            var specifications = await _context.Specifications.FindAsync(id);
            if (specifications == null)
            {
                return NotFound();
            }
            ViewData["PTID"] = new SelectList(_context.Products, "PTID", "Description", specifications.PTID);
            return View(specifications);
        }

        // POST: SellerSpecificationsManagerment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("SNID,SpecName,Picture,Weight,Size,LeadDay,PackageSize,Freebie,PTID")] Specifications specifications)
        {
            if (id != specifications.SNID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specifications);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecificationsExists(specifications.SNID))
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
            ViewData["PTID"] = new SelectList(_context.Products, "PTID", "Description", specifications.PTID);
            return View(specifications);
        }

        // GET: SellerSpecificationsManagerment/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Specifications == null)
            {
                return NotFound();
            }

            var specifications = await _context.Specifications
                .Include(s => s.PT)
                .FirstOrDefaultAsync(m => m.SNID == id);
            if (specifications == null)
            {
                return NotFound();
            }

            return View(specifications);
        }

        // POST: SellerSpecificationsManagerment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Specifications == null)
            {
                return Problem("Entity set 'OllieShopContext.Specifications'  is null.");
            }
            var specifications = await _context.Specifications.FindAsync(id);
            if (specifications != null)
            {
                _context.Specifications.Remove(specifications);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecificationsExists(long id)
        {
          return (_context.Specifications?.Any(e => e.SNID == id)).GetValueOrDefault();
        }
    }
}
