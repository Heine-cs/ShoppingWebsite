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
    public class CustomersFrontEndPageController : Controller
    {
        private readonly OllieShopContext _context;

        public CustomersFrontEndPageController(OllieShopContext context)
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
        public async Task<IActionResult> Create([Bind("CRID,URID")] Customers customers,bool roadSwitch)
        {
            if (ModelState.IsValid)
            {
                if (roadSwitch == true){ //roadSwitch是源自用戶初次註冊的action而來，初次註冊時能選擇身分，身分選擇分歧時就要走到不同的action
                                         //如果選擇只需要註冊用戶身分，就會走到這條路

                    _context.Add(customers);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create));//暫時先這樣，要回到登入頁面，但還沒製作
                }
                else //如果選擇業者與消費者身分全都要，就會走這條路
                {
                    _context.Add(customers);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Create", "SellersFrontEndPage");
                }
            }

            return View(customers);
        }

        private bool CustomersExists(long id)
        {
          return (_context.Customers?.Any(e => e.CRID == id)).GetValueOrDefault();
        }
    }
}
