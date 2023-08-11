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
    public class CustomerCouponsController : Controller
    {
        private readonly OllieShopContext _context;

        public CustomerCouponsController(OllieShopContext context)
        {
            _context = context;
        }

        // GET: CustomerCoupons
        public async Task<IActionResult> Index()
        {
            var ollieShopContext = _context.CustomerCoupons.Include(c => c.CN).Include(c => c.CR);
            return View(await ollieShopContext.ToListAsync());
        }
        //可能會用到，先註解
        // GET: CustomerCoupons/Details/5
        //public async Task<IActionResult> Details(long? id)
        //{
        //    if (id == null || _context.CustomerCoupons == null)
        //    {
        //        return NotFound();
        //    }

        //    var customerCoupons = await _context.CustomerCoupons
        //        .Include(c => c.CN)
        //        .Include(c => c.CR)
        //        .FirstOrDefaultAsync(m => m.CRCNID == id);
        //    if (customerCoupons == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(customerCoupons);
        //}

    }
}
