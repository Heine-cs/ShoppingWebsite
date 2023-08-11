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
    public class CustomerCouponsBackEndPageController : Controller
    {
        private readonly OllieShopContext _context;

        public CustomerCouponsBackEndPageController(OllieShopContext context)
        {
            _context = context;
        }

        // GET: CustomerCouponsBackEndPage
        public async Task<IActionResult> Index(long CRID)
        {
            var ollieShopContext = _context.CustomerCoupons.Include(c => c.CN).Include(c => c.CR).Where(c => c.CRID == CRID);
            return View(await ollieShopContext.ToListAsync());
        }



        // GET: CustomerCouponsBackEndPage/Create
        public IActionResult Create(long CRID)
        {
            ViewData["CRID"] = CRID;
            ViewData["DateAdded"] = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            ViewData["CNID"] = new SelectList(_context.Coupons, "CNID", "CODE");
            
            return View();
        }

        // POST: CustomerCouponsBackEndPage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CRCNID,DateAdded,AppliedDate,CNID,CRID")] CustomerCoupons customerCoupons)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerCoupons);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CNID"] = new SelectList(_context.Coupons, "CNID", "CODE", customerCoupons.CNID);
            ViewData["CRID"] = new SelectList(_context.Customers, "CRID", "CRID", customerCoupons.CRID);
            return View(customerCoupons);
        }

    }
}
