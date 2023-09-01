﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OllieShop.Models;

namespace OllieShop.Controllers
{
    public class CustomerOrdersManagementController : Controller
    {
        private readonly OllieShopContext _context;

        public CustomerOrdersManagementController(OllieShopContext context)
        {
            _context = context;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(long CRID)
        {
            if(CRID == 0)
            {
                return NotFound();
            }

            var ollieShopContext = _context.Orders
                .Include(o => o.AS)
                .Include(o => o.CN)
                .Include(o => o.CR)
                .Include(o => o.PC)
                .Include(o => o.PM)
                .Include(o => o.SR)
                .Include(o => o.SV)
                .Where(o=>o.CRID == CRID)
                .OrderByDescending(o => o.OrderDate);
            return View(await ollieShopContext.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.AS)
                .Include(o => o.CN)
                .Include(o => o.CR)
                .Include(o => o.PC)
                .Include(o => o.PM)
                .Include(o => o.SR)
                .Include(o => o.SV)
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.ORID == id);
            if (orders == null)
            {
                return NotFound();
            }

            Products product = new Products();
            List<Products> purchaseProductsInfo = new List<Products>();
            Specifications Specification = new Specifications();
            List<Specifications> purchaseProductsSpecificationInfo = new List<Specifications>();

            foreach(var item in orders.OrderDetails)
            {
                product = _context.Products.FirstOrDefault(p => p.PTID == item.PTID);
                purchaseProductsInfo.Add(product);
                Specification = _context.Specifications.FirstOrDefault(p => p.SNID == item.SNID);
                purchaseProductsSpecificationInfo.Add(Specification);
            }
            ViewData["purchaseProductsInfo"] = purchaseProductsInfo;
            ViewData["purchaseProductsSpecificationInfo"] = purchaseProductsSpecificationInfo;
            return View(orders);
        }



        // GET: CustomerOrdersManagement/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        // POST: CustomerOrdersManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ORID,OrderDate,PaymentDate,ShippedDate,ArrivalDate,Canceled,Locked,SVID,CRID,ASID,PCID,SRID,PMID,CNID")] Orders orders)
        {
            if (id != orders.ORID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.ORID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new {CRID= orders.CRID});
            }
            return View(orders);
        }

        private bool OrdersExists(long id)
        {
          return _context.Orders.Any(e => e.ORID == id);
        }
    }
}
