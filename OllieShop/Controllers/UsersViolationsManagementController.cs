using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OllieShop.Models;
using static Azure.Core.HttpHeader;

namespace OllieShop.Controllers
{
    public class UsersViolationsManagementController : Controller
    {
        private readonly OllieShopContext _context;
        private readonly IdentityCheck _identityCheck;

        public UsersViolationsManagementController(OllieShopContext context, IdentityCheck identityCheck)
        {
            _context = context;
            _identityCheck = identityCheck;
        }

        // GET: UsersViolationsManagement
        public async Task<IActionResult> Index(long URID)
        {
            //驗證使用此action對象之用戶身分是否與session資料相符
            IActionResult result = await _identityCheck.UserIdentityCheckAsync(URID);
            if (result is NotFoundResult)
            {
                return NotFound();
            }

            var ollieShopContext = await _context.Violations
                                    .Where(v => v.Submitter == URID)
                                    .Include(v => v.AD)
                                    .ToListAsync();
            List<string> suspectsName = new List<string>();
            Users singleUserQuery = null;
            foreach (var item in ollieShopContext)
            {
                singleUserQuery = await _context.Users.FirstOrDefaultAsync(u => u.URID == item.Suspect);
                suspectsName.Add(singleUserQuery.Name);
            }

            ViewData["suspectsName"] = suspectsName;
            return View(ollieShopContext);
        }

        // GET: UsersViolationsManagement/Details/5
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
            //驗證使用此action對象之用戶身分是否與session資料相符
            IActionResult result = await _identityCheck.UserIdentityCheckAsync(violations.Submitter.GetValueOrDefault());
            if (result is NotFoundResult)
            {
                return NotFound();
            }
            return View(violations);
        }

        // GET: UsersViolationsManagement/Create
        public async Task<IActionResult> Create(long SubmitterUserID, long SuspectUserID)
        {
            ViewData["SubmitterUserID"] = SubmitterUserID;
            //驗證使用此action對象之用戶身分是否與session資料相符
            IActionResult result = await _identityCheck.UserIdentityCheckAsync(SubmitterUserID);
            if (result is NotFoundResult)
            {
                return NotFound();
            }
            ViewData["SuspectUserID"] = SuspectUserID;
            ViewData["SuspectName"] = await  _context.Users
                                        .Where(u => u.URID == SuspectUserID)
                                        .Select(u => u.Name)
                                        .FirstOrDefaultAsync();
            ViewData["SubmitDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return View();
        }

        // POST: UsersViolationsManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VioID,Submitter,Suspect,Reason,SubmitDate,Disabled,ADID")] Violations violations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(violations);
                await _context.SaveChangesAsync();
                TempData["CreateSuccessMessage"] = "成功提交檢舉!!";
                return RedirectToAction(nameof(Index), new { URID = violations.Submitter });
            }

            ViewData["SubmitterUserID"] = violations.Submitter;
            ViewData["SuspectName"] = violations.Suspect;

            return View(violations);
        }

        //GET: UsersViolationsManagement/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Violations == null)
            {
                return NotFound();
            }

            var violations = await _context.Violations.FindAsync(id);
            if (violations == null)
            {
                return NotFound();
            }

            //驗證使用此action對象之用戶身分是否與session資料相符
            IActionResult result = await _identityCheck.UserIdentityCheckAsync(violations.Submitter.GetValueOrDefault());
            if (result is NotFoundResult)
            {
                return NotFound();
            }

            ViewData["ADID"] = violations.ADID;
            ViewData["Submitter"] = violations.Submitter;
            ViewData["Suspect"] = violations.Suspect;
            ViewData["SuspectName"] = await _context.Users
                                        .Where(u => u.URID == violations.Suspect)
                                        .Select(u => u.Name)
                                        .FirstOrDefaultAsync();
            return View(violations);
        }

        //POST: UsersViolationsManagement/Edit/5
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
                return RedirectToAction(nameof(Index), new {URID = violations.Submitter});
            }
            ViewData["ADID"] = violations.ADID;
            ViewData["Submitter"] = violations.Submitter;
            ViewData["Suspect"] = violations.Suspect;
            ViewData["SuspectName"] = await _context.Users
                            .Where(u => u.URID == violations.Suspect)
                            .Select(u => u.Name)
                            .FirstOrDefaultAsync();
            return View(violations);
        }

        // GET: UsersViolationsManagement/Delete/5
        public async Task<IActionResult> Delete(long? id)
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
            //驗證使用此action對象之用戶身分是否與session資料相符
            IActionResult result = await _identityCheck.UserIdentityCheckAsync(violations.Submitter.GetValueOrDefault());
            if (result is NotFoundResult)
            {
                return NotFound();
            }
            return View(violations);
        }

        // POST: UsersViolationsManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Violations == null)
            {
                return Problem("Entity set 'OllieShopContext.Violations'  is null.");
            }
            var violations = await _context.Violations.FindAsync(id);
            if (violations != null)
            {
                _context.Violations.Remove(violations);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { URID = violations.Submitter });
        }

        private bool ViolationsExists(long id)
        {
          return _context.Violations.Any(e => e.VioID == id);
        }
    }
}
