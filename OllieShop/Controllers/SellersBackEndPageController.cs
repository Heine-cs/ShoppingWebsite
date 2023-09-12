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
        private readonly IdentityCheck _identityCheck;
        public SellersBackEndPageController(OllieShopContext context, IdentityCheck identityCheck)
        {
            _context = context;
            _identityCheck = identityCheck;
        }

        // GET: SellersBackEndPage/Details/5
        public async Task<IActionResult> Details(long? SRID)
        {
            if (SRID == null || _context.Sellers == null)
            {
                return NotFound();
            }

            //驗證使用此action對象之賣家身分是否與session資料相符
            IActionResult result = await _identityCheck.SellerIdentityCheckAsync(SRID.GetValueOrDefault());
            if (result is NotFoundResult)
            {
                return NotFound();
            }

            var sellers = await _context.Sellers
                .Include(s => s.UR)
                .FirstOrDefaultAsync(m => m.SRID == SRID);
            if (sellers == null)
            {
                return NotFound();
            }

            return View(sellers);
        }

        public async Task<IActionResult> Edit(long? SRID)
        {
            if (SRID == null || _context.Sellers == null)
            {
                return NotFound();
            }
            //驗證使用此action對象之賣家身分是否與session資料相符
            IActionResult result = await _identityCheck.SellerIdentityCheckAsync(SRID.GetValueOrDefault());
            if (result is NotFoundResult)
            {
                return NotFound();
            }
            var sellers = await _context.Sellers.FindAsync(SRID);
            if (sellers == null)
            {
                return NotFound();
            }
            ViewData["URID"] = sellers.URID;
            return View(sellers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("SRID,ShopNAME,TaxID,BankCode,BankAccount,URID,Picture")] Sellers sellers)
        {
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
                return RedirectToAction(nameof(Details), new {sellers.SRID});
            }
            return View(sellers);
        }


        private bool SellersExists(long id)
        {
          return (_context.Sellers?.Any(e => e.SRID == id)).GetValueOrDefault();
        }

        public IActionResult OptionsMenu()
        {
            return View();
        }
    }
}
