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
        private readonly IdentityCheck _identityCheck;
        public CustomerCouponsBackEndPageController(OllieShopContext context, IdentityCheck identityCheck)
        {
            _context = context;
            _identityCheck = identityCheck;
        }

        // GET: CustomerCouponsBackEndPage
        public async Task<IActionResult> Index(long CRID)
        {
            //驗證使用此action對象之買家身分是否與session資料相符
            IActionResult result = await _identityCheck.CustomerIdentityCheckAsync(CRID);
            if (result is NotFoundResult)
            {
                return NotFound();
            }

            var ollieShopContext = _context.CustomerCoupons.Include(c => c.CN).Include(c => c.CR)
                                                            .Where(c => c.CRID == CRID)
                                                            .OrderByDescending(c => c.DateAdded);
            ViewData["CRID"] = CRID;
            return View(await ollieShopContext.ToListAsync());
        }



        // GET: CustomerCouponsBackEndPage/Create
        public async Task<IActionResult> Create(long CRID)
        {
            //驗證使用此action對象之買家身分是否與session資料相符
            IActionResult result = await _identityCheck.CustomerIdentityCheckAsync(CRID);
            if (result is NotFoundResult)
            {
                return NotFound();
            }

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
            var couponQuery =from s in _context.Coupons
                                where ((s.CODE == CODE))
                                select new { s.CNID, s.ExpiryDate};
            // 检查查询是否返回了任何结果
            if (couponQuery.Any())
            {
                long CNID = couponQuery.FirstOrDefault().CNID;
                // 查询 CustomerCoupons 表检查优惠券 ID（CNID）是否已存在當前CRID之下，已存在就不能新增重複優惠券
                var CouponIdExistInCustomerCouponTableStatusQuery = from s in _context.CustomerCoupons
                                                                    where (s.CNID == CNID && s.CRID == customerCoupons.CRID)
                                                                    select s.CNID;
                //折價券沒有逾期才能新增
                if (couponQuery.FirstOrDefault().ExpiryDate > DateTime.Now)
                {
                    if (!CouponIdExistInCustomerCouponTableStatusQuery.Any())
                    {
                        customerCoupons.CNID = CNID;
                        _context.Add(customerCoupons);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index", new { customerCoupons.CRID });
                    }
                    ViewData["RepeatCoupon"] = "已擁有該折價券代碼之票券";
                }
                else
                {
                    ViewData["ExpiredCoupon"] = "折價券已逾期，不得新增";
                }
            }
            else { 
                ViewData["InvalidCoupon"] = "輸入折價券代碼錯誤，請重新輸入"; 
            }
            ViewData["CODE"] = CODE;
            ViewData["CRID"] = customerCoupons.CRID;
            ViewData["DateAdded"] = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            return View(customerCoupons);
        }
    }
}
