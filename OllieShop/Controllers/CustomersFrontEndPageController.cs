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
        private readonly ILogger<CustomersFrontEndPageController> _logger;
        public CustomersFrontEndPageController(OllieShopContext context, ILogger<CustomersFrontEndPageController> logger)
        {
            _context = context;
            _logger = logger;
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
                //_context.Add(customers);
                //await _context.SaveChangesAsync();
                if (roadSwitch == true) { //roadSwitch是源自用戶初次註冊的action而來，初次註冊時能選擇身分，身分選擇分歧時就要走到不同的action
                                          //如果選擇只需要註冊用戶身分，就會走到這條路
                    return RedirectToAction(nameof(CustomersAgreeTerms), new { URID = customers.URID, purpose = "BecomeCustomer" });
                }
                else //如果選擇業者與消費者身分全都要，就會走這條路
                {
                    return RedirectToAction(nameof(CustomersAgreeTerms), new { URID = customers.URID, purpose = "BecomeCustomerAndSeller" });
                }
            }
            return View(customers);
        }
        
        public IActionResult CustomersAgreeTerms(long URID, string purpose)
        {
            ViewData["URID"] = URID;
            ViewData["purpose"] = purpose;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomersAgreeTerms([Bind("CRID,URID")] Customers customers,string purpose)
        {
            _context.Add(customers);
            await _context.SaveChangesAsync();
            if (purpose== "BecomeCustomer") {
                try
                {
                    return RedirectToAction("ReLogin", "Accounts");
                }
                catch (Exception ex)
                {
                    // 使用 ILogger 記錄異常消息
                    _logger.LogError(ex, "An error occurred");
                    return View("Error");
                }
            }
            try
            {
                return RedirectToAction("SellersAgreeTerms", "SellersFrontEndPage", new { customers.URID});
            }
            catch (Exception ex)
            {
                // 使用 ILogger 記錄異常消息
                _logger.LogError(ex, "An error occurred");
                return View("Error");
            }
        }
    }
}
