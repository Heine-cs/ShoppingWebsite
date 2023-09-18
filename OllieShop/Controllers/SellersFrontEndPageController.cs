using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OllieShop.Models;
using OllieShop.ViewComponents;

namespace OllieShop.Controllers
{
    public class SellersFrontEndPageController : Controller
    {
        private readonly OllieShopContext _context;
        private readonly ILogger<CustomersFrontEndPageController> _logger;
        public SellersFrontEndPageController(OllieShopContext context, ILogger<CustomersFrontEndPageController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(long SRID)
        {
            List<Products> sellerProductsWithSpecification = await _context.Products
                                                                            .Include(p => p.Specifications)
                                                                            .Where(p => p.SRID == SRID).ToListAsync();
            ViewData["ShopName"] = _context.Sellers
                                            .Where(s => s.SRID == SRID)
                                            .Select(s=>s.ShopNAME)
                                            .FirstOrDefault()
                                            .ToString();

            //為了呼叫Sellers ViewComponent存在的參數
            ViewData["SRID"] = SRID;

            //為了檢舉賣家需要用到的UserID
            ViewData["SellerUserID"] = _context.Sellers
                                        .Where(s => s.SRID == SRID)
                                        .Select(s => s.URID)
                                        .FirstOrDefault();

            return View(sellerProductsWithSpecification);
        }

        public IActionResult Create(long URID)
        {
            ViewData["URID"] = URID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SRID,ShopNAME,TaxID,BankCode,BankAccount,URID,Picture")] Sellers sellers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sellers);
                await _context.SaveChangesAsync();
                return RedirectToAction("ReLogin", "Accounts");
            }

            return View(sellers);
        }

        public IActionResult SellersAgreeTerms(long URID)
        {
            ViewData["URID"] = URID;
            return View();
        }
    }
}
