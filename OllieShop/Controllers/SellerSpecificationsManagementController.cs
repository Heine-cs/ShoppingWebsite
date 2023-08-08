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

        public IActionResult Create(long SRID,long PTID,string productName)
        {
            ViewData["SRID"] = SRID;//上傳圖片到業者個人圖庫會用到此參數

            ViewData["PTID"] = PTID;//存規格資料表會需要此作為外鍵
            ViewData["productName"] = productName;//在新增規格頁顯示產品名稱，data來源自SellerProducts Create頁
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
                return RedirectToAction("Edit","SellerProductsManagement", new { id = specifications.PTID});
            }
            ViewData["PTID"] = specifications.PTID;
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
            //撈出SRID用意是要傳給Edit頁面上傳圖片時要存入對應業者的資料夾，會需要用到這個SRID組合路徑，
            //其程式碼為 url: "@Url.Action("photoUpload", "FileUpload",new {SRID=@ViewBag.SRID})",
            var productInfo = await _context.Products.Where(p => p.PTID == specifications.PTID).ToListAsync();
            if (productInfo == null)
            {
                return NotFound();
            }
            
            ViewData["SRID"] = productInfo[0].SRID;

            ViewData["PTID"] = specifications.PTID;
            return View(specifications);
        }

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

                return RedirectToAction("Edit", "SellerProductsManagement", new { id = specifications.PTID });
            }
            ViewData["PTID"] = specifications.PTID;
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
