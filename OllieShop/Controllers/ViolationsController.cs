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
    public class ViolationsController : Controller
    {
        private readonly OllieShopContext _context;
        private readonly IdentityCheck _identityCheck;

        public ViolationsController(OllieShopContext context,IdentityCheck identityCheck)
        {
            _context = context;
            _identityCheck = identityCheck;
        }

        public async Task<IActionResult> Index()
        {
            var ollieShopContext = _context.Violations.Include(v => v.AD).OrderByDescending(v=>v.SubmitDate);
            return View(await ollieShopContext.ToListAsync());
        }

        public async Task<IActionResult> unhandleViolationsPage()
        {
            var ollieShopContext = _context.Violations.Include(v => v.AD).Where(v => v.Disabled == null);
            return View(await ollieShopContext.ToListAsync());
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Violations == null)
            {
                return NotFound();
            }

            var violations = await _context.Violations
                .Include(v => v.AD)
                .FirstOrDefaultAsync(m => m.VioID == id);
            if (violations == null)
            {
                return NotFound();
            }

            return View(violations);
        }

        public async Task<IActionResult> Edit(long? id,short ADID)
        {
            if (id == null || _context.Violations == null)
            {
                return NotFound();
            }

            //驗證使用此action對象之管理員身分是否與session資料相符
            IActionResult result = await _identityCheck.AdminCheckAsync(ADID);
            if(result is NotFoundResult)
            {
                return NotFound();
            }

            var violations = await _context.Violations.FindAsync(id);
            if (violations == null)
            {
                return NotFound();
            }
            ViewData["ADID"] = ADID;
            return View(violations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("VioID,Submitter,Suspect,Reason,SubmitDate,Disabled,ADID")] Violations violations)
        {
            if (id != violations.VioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(violations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViolationsExists(violations.VioID))
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
            ViewData["ADID"] = new SelectList(_context.Admins, "ADID", "Account", violations.ADID);
            ViewData["Submitter"] = new SelectList(_context.Users, "URID", "URID", violations.Submitter);
            ViewData["Suspect"] = new SelectList(_context.Users, "URID", "URID", violations.Suspect);
            return View(violations);
        }

        private bool ViolationsExists(long id)
        {
          return (_context.Violations?.Any(e => e.VioID == id)).GetValueOrDefault();
        }
    }
}
