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
            var ollieShopContext = _context.CustomerCoupons.Include(c => c.CN).Include(c => c.CR)
                                                            .Where(c => c.CRID == CRID)
                                                            .OrderByDescending(c => c.DateAdded);
            return View(await ollieShopContext.ToListAsync());
        }



        // GET: CustomerCouponsBackEndPage/Create
        public IActionResult Create(long CRID)
        {
            ViewData["CRID"] = CRID;
            ViewData["DateAdded"] = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            return View();
        }

        // POST: CustomerCouponsBackEndPage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CRCNID,DateAdded,AppliedDate,CNID,CRID,CODE")] CustomerCoupons customerCoupons,string CODE)
        {

                // 从数据库中查询基于提供的 CODE 检索优惠券 ID（CNID）
                var couponIdQuery =from s in _context.Coupons
                                   where ((s.CODE == CODE))
                                   select s.CNID;
                // 检查查询是否返回了任何结果
                if (couponIdQuery.Any()) {
                    long CNID= couponIdQuery.FirstOrDefault();
                    // 查询 CustomerCoupons 表以检查优惠券 ID（CNID）是否已存在，已存在就不能新增重複優惠券
                    var CouponIdExistInCustomerCouponTableStatusQuery = from s in _context.CustomerCoupons
                                        where ((s.CNID == CNID))
                                        select s.CNID;
                    if (!CouponIdExistInCustomerCouponTableStatusQuery.Any()) { 
                        customerCoupons.CNID = CNID;
                        _context.Add(customerCoupons);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index", new {CRID = customerCoupons.CRID});
                    }
                }
            ViewData["CODE"] = CODE;
            ViewData["CRID"] = customerCoupons.CRID;
            ViewData["DateAdded"] = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            ViewData["ErrorMessage"] = couponIdQuery.Any() == false? "輸入折價券代碼錯誤，請重新輸入":"已擁有該折價券代碼之票券";
            return View(customerCoupons);
        }
    }
}
