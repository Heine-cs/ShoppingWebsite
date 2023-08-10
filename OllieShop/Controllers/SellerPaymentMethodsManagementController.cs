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

        public SellerPaymentMethodsManagementController(OllieShopContext context)
        {
            _context = context;
        }

        // GET: SellerPaymentMethodsManagement
        public async Task<IActionResult> Index(long SRID)
        {
            SRID = 2;//先寫死，要修改
            var ollieShopContext = _context.SellerPaymentMethods.Include(s => s.PM).Include(s => s.SR).Where(s =>s.SRID == SRID);
            ViewData["SRID"] = SRID;
            return View(await ollieShopContext.ToListAsync());
        }

        // GET: SellerPaymentMethodsManagement/Create
        public IActionResult Create(long SRID)
        {
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
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["PMInfo"] = new SelectList(_context.PaymentMethods, "PMID", "Name", sellerPaymentMethods.PMID);
            ViewData["SRID"] = new SelectList(_context.Sellers, "SRID", "SRID", sellerPaymentMethods.SRID);
            ViewData["ErrorMessage"] = "已經綁定此種付款方式，不能再次綁定既存付款方式";
            return View(sellerPaymentMethods);
        }

        // GET: SellerPaymentMethodsManagement/Edit/5
        public async Task<IActionResult> Edit(long? SRID,string PMID)
        {
            if (SRID == null || PMID == null ||_context.SellerPaymentMethods == null)
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
            return View(sellerPaymentMethods);
        }

        // POST: SellerPaymentMethodsManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SellerPaymentMethodsExists(long id)
        {
          return (_context.SellerPaymentMethods?.Any(e => e.SRID == id)).GetValueOrDefault();
        }
    }
}
