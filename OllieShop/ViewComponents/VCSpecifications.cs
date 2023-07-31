using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OllieShop.Models;

namespace OllieShop.ViewComponents
{
    public class VCSpecifications: ViewComponent
    {
        //建立描述資料庫的程式碼，建立後再讀取資料庫的資料表
        private readonly OllieShopContext _context;

        public VCSpecifications(OllieShopContext context)
        {
            _context = context;
        }
        //收到參數後撈商品所有規格資料回傳呼叫者
        public async Task <IViewComponentResult> InvokeAsync(long ptid)
        {
            var specifications = await _context.Specifications.Where(a => a.PTID == ptid).ToListAsync();

            return View(specifications);
        }
    }
}
