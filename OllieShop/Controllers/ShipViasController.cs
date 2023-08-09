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
    public class ShipViasController : Controller
    {
        private readonly OllieShopContext _context;

        public ShipViasController(OllieShopContext context)
        {
            _context = context;
        }

        // GET: ShipVias
        public async Task<IActionResult> Index()
        {
            var ollieShopContext = _context.ShipVias.Include(s => s.AD);
            return View(await ollieShopContext.ToListAsync());
        }

        // GET: ShipVias/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ShipVias == null)
            {
                return NotFound();
            }

            var shipVias = await _context.ShipVias
                .Include(s => s.AD)
                .FirstOrDefaultAsync(m => m.SVID == id);
            if (shipVias == null)
            {
                return NotFound();
            }

            return View(shipVias);
        }

        // GET: ShipVias/Create
        public IActionResult Create()
        {
            ViewData["ADID"] = new SelectList(_context.Admins, "ADID", "Account");
            return View();
        }

        // POST: ShipVias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SVID,Name,ADID")] ShipVias shipVias)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipVias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ADID"] = new SelectList(_context.Admins, "ADID", "Account", shipVias.ADID);
            return View(shipVias);
        }

        // GET: ShipVias/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ShipVias == null)
            {
                return NotFound();
            }

            var shipVias = await _context.ShipVias.FindAsync(id);
            if (shipVias == null)
            {
                return NotFound();
            }
            ViewData["ADID"] = new SelectList(_context.Admins, "ADID", "Account", shipVias.ADID);
            return View(shipVias);
        }

        // POST: ShipVias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SVID,Name,ADID")] ShipVias shipVias)
        {
            if (id != shipVias.SVID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipVias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipViasExists(shipVias.SVID))
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
            ViewData["ADID"] = new SelectList(_context.Admins, "ADID", "Account", shipVias.ADID);
            return View(shipVias);
        }

        // GET: ShipVias/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ShipVias == null)
            {
                return NotFound();
            }

            var shipVias = await _context.ShipVias
                .Include(s => s.AD)
                .FirstOrDefaultAsync(m => m.SVID == id);
            if (shipVias == null)
            {
                return NotFound();
            }

            return View(shipVias);
        }

        // POST: ShipVias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ShipVias == null)
            {
                return Problem("Entity set 'OllieShopContext.ShipVias'  is null.");
            }
            var shipVias = await _context.ShipVias.FindAsync(id);
            if (shipVias != null)
            {
                _context.ShipVias.Remove(shipVias);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipViasExists(string id)
        {
          return (_context.ShipVias?.Any(e => e.SVID == id)).GetValueOrDefault();
        }
    }
}
