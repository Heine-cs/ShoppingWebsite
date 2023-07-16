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
    public class CategorysController : Controller
    {
        private readonly OllieShopContext _context;

        public CategorysController(OllieShopContext context)
        {
            _context = context;
        }

        // GET: Categorys
        public async Task<IActionResult> Index()
        {
            var ollieShopContext = _context.Categorys.Include(c => c.AD);
            return View(await ollieShopContext.ToListAsync());
        }

        // GET: Categorys/Create
        public IActionResult Create()
        {
            ViewData["ADID"] = new SelectList(_context.Admins, "ADID", "ADID");
            return View();
        }

        // POST: Categorys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CYID,Name,ADID")] Categorys categorys)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categorys);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ADID"] = new SelectList(_context.Admins, "ADID", "ADID", categorys.ADID);
            return View(categorys);
        }

        // GET: Categorys/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Categorys == null)
            {
                return NotFound();
            }

            var categorys = await _context.Categorys.FindAsync(id);
            if (categorys == null)
            {
                return NotFound();
            }
            ViewData["ADID"] = new SelectList(_context.Admins, "ADID", "ADID", categorys.ADID);
            return View(categorys);
        }

        // POST: Categorys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CYID,Name,ADID")] Categorys categorys)
        {
            if (id != categorys.CYID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categorys);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategorysExists(categorys.CYID))
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
            ViewData["ADID"] = new SelectList(_context.Admins, "ADID", "ADID", categorys.ADID);
            return View(categorys);
        }

        // GET: Categorys/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Categorys == null)
            {
                return NotFound();
            }

            var categorys = await _context.Categorys
                .Include(c => c.AD)
                .FirstOrDefaultAsync(m => m.CYID == id);
            if (categorys == null)
            {
                return NotFound();
            }

            return View(categorys);
        }

        // POST: Categorys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Categorys == null)
            {
                return Problem("Entity set 'OllieShopContext.Categorys'  is null.");
            }
            var categorys = await _context.Categorys.FindAsync(id);
            if (categorys != null)
            {
                _context.Categorys.Remove(categorys);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategorysExists(string id)
        {
          return (_context.Categorys?.Any(e => e.CYID == id)).GetValueOrDefault();
        }
    }
}
