using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OllieShop.Models;

namespace OllieShop.ViewComponents
{
    public class VCAccounts: ViewComponent
    {
        //建立描述資料庫的程式碼，建立後再讀取資料庫的資料表
        private readonly OllieShopContext _context;

        public VCAccounts(OllieShopContext context)
        {
            _context = context;
        }

        //收到參數後撈用戶所有帳戶資料回傳呼叫者
        public async Task <IViewComponentResult> InvokeAsync(long urid)
        {
            var accounts = await _context.Accounts.Where(a=>a.URID == urid).ToListAsync();

            //密碼轉成*字號顯示，每個帳戶塞進accordionGenerator method生成摺疊清單
            Accounts hiddenpassword = new Accounts();
            foreach (var account in accounts)
            {
                account.Password = hiddenpassword.GetMaskedPassword(account.Password);
            }

            return View(accounts);
        }

    }
}
