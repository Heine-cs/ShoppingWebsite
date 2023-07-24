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
    public class SellersFrontEndPageController : Controller
    {
        private readonly OllieShopContext _context;

        public SellersFrontEndPageController(OllieShopContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            TempData["forUserIdentity"] = TempData["forUserIdentity"];//接受來自用戶註冊action，裡面放該用戶的URID
                                                                      //因為TempData生命週期只能跨越控制器一次，User註冊後產生的TempData陽壽燒完了，這裡續命接關
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SRID,ShopNAME,TaxID,BankCode,BankAccount,URID")] Sellers sellers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sellers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));//暫時先這樣，要回到登入頁面，但還沒製作
            }

            return View(sellers);
        }



        private bool SellersExists(long id)
        {
          return (_context.Sellers?.Any(e => e.SRID == id)).GetValueOrDefault();
        }
    }
}
