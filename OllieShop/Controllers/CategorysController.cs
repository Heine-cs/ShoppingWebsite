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
        private readonly IdentityCheck _identityCheck;
        public CategorysController(OllieShopContext context, IdentityCheck identityCheck)
        {
            _context = context;
            _identityCheck = identityCheck;
        }

        // GET: Categorys
        public async Task<IActionResult> Index()
        {
            var ollieShopContext = _context.Categorys.Include(c => c.AD);
            return View(await ollieShopContext.ToListAsync());
        }

        // GET: Categorys/Create
        public async Task<IActionResult> Create(short ADID)
        {
            //驗證使用此Action的對象是否與session儲存帳戶資料相符
            IActionResult result = await _identityCheck.AdminCheckAsync(ADID);
            if (result is NotFoundResult)
            {
                return NotFound();
            }
            ViewData["ADID"] = ADID;
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
            ViewData["ADID"] = categorys.ADID;
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
            //驗證使用此Action的對象是否與session儲存帳戶資料相符
            IActionResult result = await _identityCheck.AdminCheckAsync(categorys.ADID.GetValueOrDefault());
            if (result is NotFoundResult)
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
