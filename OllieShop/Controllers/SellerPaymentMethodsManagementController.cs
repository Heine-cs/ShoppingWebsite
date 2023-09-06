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
    public class SellerPaymentMethodsManagementController : Controller
    {
        private readonly OllieShopContext _context;
        private readonly IdentityCheck _identityCheck;
        public SellerPaymentMethodsManagementController(OllieShopContext context, IdentityCheck identityCheck)
        {
            _context = context;
            _identityCheck = identityCheck;
        }

        // GET: SellerPaymentMethodsManagement
        public async Task<IActionResult> Index(long SRID)
        {
            //驗證使用此action對象之賣家身分是否與session資料相符
            IActionResult result = await _identityCheck.SellerIdentityCheckAsync(SRID);
            if (result is NotFoundResult)
            {
                return NotFound();
            }

            var ollieShopContext = _context.SellerPaymentMethods.Include(s => s.PM).Include(s => s.SR).Where(s =>s.SRID == SRID);
            if (ollieShopContext.Count() == 0)
            {
                ViewData["SellerPaymentMethodsNotBind"] = "尚未綁定任何付款方式";
            }
            ViewData["SRID"] = SRID;
            return View(await ollieShopContext.ToListAsync());
        }

        // GET: SellerPaymentMethodsManagement/Create
        public async Task<IActionResult> Create(long SRID)
        {
            //驗證使用此action對象之賣家身分是否與session資料相符
            IActionResult result = await _identityCheck.SellerIdentityCheckAsync(SRID);
            if (result is NotFoundResult)
            {
                return NotFound();
            }

            ViewData["PMInfo"] = new SelectList(_context.PaymentMethods, "PMID", "Name");
            ViewData["SRID"] = SRID;
            return View();
        }

        // POST: SellerPaymentMethodsManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SRID,PMID,Canceled")] SellerPaymentMethods sellerPaymentMethods)
        {
            //撈出資料庫與sellerPaymentMethods相符的資料，只是為了判斷是否存在，存在就不能進行POST
            var compareQuery = from s in _context.SellerPaymentMethods
                        where ((s.SRID == sellerPaymentMethods.SRID && s.PMID == sellerPaymentMethods.PMID))
                        select s;
            if(!compareQuery.Any()) { 
                if (ModelState.IsValid)
                {
                    _context.Add(sellerPaymentMethods);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new {sellerPaymentMethods.SRID});
                }
            }
            ViewData["PMInfo"] = new SelectList(_context.PaymentMethods, "PMID", "Name", sellerPaymentMethods.PMID);
            ViewData["SRID"] = sellerPaymentMethods.SRID;
            ViewData["ErrorMessage"] = "已經擁有此種付款方式，不能再次新增既存付款方式";
            return View(sellerPaymentMethods);
        }

        // GET: SellerPaymentMethodsManagement/Edit/5
        public async Task<IActionResult> Edit(long? SRID,string PMID,string PaymentMethodsName)
        {
            if (SRID == null || PMID == null ||_context.SellerPaymentMethods == null)
            {
                return NotFound();
            }
            //驗證使用此action對象之賣家身分是否與session資料相符
            IActionResult result = await _identityCheck.SellerIdentityCheckAsync(SRID.GetValueOrDefault());
            if (result is NotFoundResult)
            {
                return NotFound();
            }
            var sellerPaymentMethods = await _context.SellerPaymentMethods.FindAsync(SRID,PMID);
            if (sellerPaymentMethods == null)
            {
                return NotFound();
            }
            ViewData["PMInfo"] = new SelectList(_context.PaymentMethods, "PMID", "Name", sellerPaymentMethods.PMID);
            ViewData["SRID"] = SRID;
            ViewData["PaymentMethodsName"] =  PaymentMethodsName;
            return View(sellerPaymentMethods);
        }

        // POST: SellerPaymentMethodsManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long SRID, string PMID, [Bind("SRID,PMID,Canceled")] SellerPaymentMethods sellerPaymentMethods)
        {
            if (SRID != sellerPaymentMethods.SRID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sellerPaymentMethods);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerPaymentMethodsExists(sellerPaymentMethods.SRID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { sellerPaymentMethods.SRID });
            }
            return RedirectToAction("Index", new { sellerPaymentMethods.SRID });
        }

        private bool SellerPaymentMethodsExists(long id)
        {
          return (_context.SellerPaymentMethods?.Any(e => e.SRID == id)).GetValueOrDefault();
        }
    }
}
