using Microsoft.AspNetCore.Mvc;
using OllieShop.Models;
using System.Diagnostics;

namespace OllieShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Assistant()
        {
            //協助中心View的Q&A
            Accordion accordion = new Accordion();
            List<string> Question_With_Answer = new List<string> {
            accordion.accordionGenerator("OllieShop是什麼?", "OllieShop是一個線上購物平台，提供各種商品和服務，從時尚服飾到家居用品，從電子產品到美妝保健品，滿足您的各種購物需求。", 1),
            accordion.accordionGenerator("如何在OllieShop上購物?","在OllieShop上購物非常簡單！您只需進入我們的網站，創建一個帳戶，瀏覽我們的產品分類，選擇您喜歡的商品，加入購物車，然後選擇結帳方式完成訂單。",2),
            accordion.accordionGenerator("OllieShop接受哪些支付方式?","我們接受多種支付方式，包括信用卡、貨到付款和其他線上支付平台。您可以在結帳時選擇您喜歡的支付方式",3),
            accordion.accordionGenerator("OllieShop的退換貨政策如何?","我們提供退換貨服務，以確保您對購買的商品滿意。如果您對商品有任何問題或不滿意，請在收到商品後的一周內與我們的客戶服務團隊聯繫。我們將指導您完成退換貨程序並提供必要的協助。",4),
            accordion.accordionGenerator("OllieShop的送貨方式和時間是多久?","商家提供多種送貨方式，包括標準宅配和超商取貨。送貨時間取決於您所在的地區以及您選擇的送貨方式。通常情況下需要3至7個工作日。",5),
            accordion.accordionGenerator("我可以在OllieShop上追蹤我的訂單嗎?","是的，您可以在OllieShop上追蹤您的訂單。一旦您完成訂單，我們將向您提供一個訂單追蹤號碼，您可以使用此號碼在我們的網站上追蹤您的訂單狀態。",6),
            accordion.accordionGenerator("我可以取消或修改我的訂單嗎?","是的，如果您希望取消或修改訂單，請在收到訂單確認郵件後盡快與我們的客戶服務團隊聯繫。我們將盡力滿足您的需求，但請注意，如果訂單已經處於發貨或運送階段，可能無法進行取消或修改。",7),
            accordion.accordionGenerator("我可以在OllieShop上設立自己的賣場嗎?","是的，我們歡迎商家在OllieShop上設立自己的賣場。如果您是一家企業或個人商家，有興趣在我們的平台上銷售商品，請與我們的商家合作團隊聯繫，我們將提供相關的合作資訊和支持。",8)
            };
            ViewData["Question_With_Answer"] = Question_With_Answer;
            //協助中心View的Q&A
            return View();
        }

        public IActionResult OfficialContactInfo()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}