using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OllieShop.Models;
using static NuGet.Packaging.PackagingConstants;

namespace OllieShop.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly OllieShopContext _context;
        private readonly IdentityCheck _identityCheck;

        public AnnouncementsController(OllieShopContext context, IdentityCheck identityCheck)
        {
            _context = context;
            _identityCheck = identityCheck;
        }

        // GET: Announcements
        public async Task<IActionResult> Index()
        {
            var ollieShopContext = _context.Announcements
                .OrderByDescending(a => a.PublicDate)
                .Include(a => a.AD);
            return View(await ollieShopContext.ToListAsync());
        }

        // GET: Announcements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Announcements == null)
            {
                return NotFound();
            }

            var announcements = await _context.Announcements
                .Include(a => a.AD)
                .FirstOrDefaultAsync(m => m.ATID == id);
            if (announcements == null)
            {
                return NotFound();
            }

            return View(announcements);
        }

        // GET: Announcements/Create
        public async Task<IActionResult> Create(short ADID)
        {
            //驗證使用此Action的對象是否與session儲存帳戶資料相符
            IActionResult result = await _identityCheck.AdminCheckAsync(ADID);
            if(result is NotFoundResult)
            {
                return NotFound();
            }

            ViewData["ADID"] = ADID;
            return View();
        }

        // POST: Announcements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ATID,PublicDate,ATContent,ADID,Title")] Announcements announcements)
        {
            if (ModelState.IsValid)
            {
                _context.Add(announcements);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ADID"] = new SelectList(_context.Admins, "ADID", "ADID", announcements.ADID);
            return View(announcements);
        }

        // GET: Announcements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Announcements == null)
            {
                return NotFound();
            }

            var announcements = await _context.Announcements.FindAsync(id);
            if (announcements == null)
            {
                return NotFound();
            }
            //驗證使用此Action的對象是否與session儲存帳戶資料相符
            IActionResult result = await _identityCheck.AdminCheckAsync(announcements.ADID.GetValueOrDefault());
            if (result is NotFoundResult)
            {
                return NotFound();
            }

            ViewData["ADID"] = announcements.ADID;
            return View(announcements);
        }

        // POST: Announcements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ATID,PublicDate,ATContent,ADID,Title")] Announcements announcements)
        {
            if (id != announcements.ATID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(announcements);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnouncementsExists(announcements.ATID))
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
            ViewData["ADID"] = announcements.ADID;
            return View(announcements);
        }

        // GET: Announcements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Announcements == null)
            {
                return NotFound();
            }

            var announcements = await _context.Announcements
                .Include(a => a.AD)
                .FirstOrDefaultAsync(m => m.ATID == id);
            if (announcements == null)
            {
                return NotFound();
            }
            //驗證使用此Action的對象是否與session儲存帳戶資料相符
            IActionResult result = await _identityCheck.AdminCheckAsync(announcements.ADID.GetValueOrDefault());
            if (result is NotFoundResult)
            {
                return NotFound();
            }

            return View(announcements);
        }

        // POST: Announcements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Announcements == null)
            {
                return Problem("Entity set 'OllieShopContext.Announcements'  is null.");
            }
            var announcements = await _context.Announcements.FindAsync(id);
            if (announcements != null)
            {
                _context.Announcements.Remove(announcements);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnouncementsExists(int id)
        {
          return (_context.Announcements?.Any(e => e.ATID == id)).GetValueOrDefault();
        }
    }
}
