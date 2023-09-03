using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OllieShop.Models;

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

        public IActionResult Create(long URID)
        {
            ViewData["URID"] = URID;
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
                return RedirectToAction("ReLogin", "Accounts");
            }

            return View(sellers);
        }

        public async Task<IActionResult> SellersAgreeTerms(long URID)
        {
            ViewData["URID"] = URID;
            return View();
        }
    }
}
