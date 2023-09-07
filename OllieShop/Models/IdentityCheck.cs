using Microsoft.AspNetCore.Mvc;
using OllieShop.Models;

namespace OllieShop.Models
{
    public class IdentityCheck:Object
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IdentityCheck(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> SellerIdentityCheckAsync(long SRID)
        {
            string sellerAccountInfo = _httpContextAccessor.HttpContext.Session.GetString("SellerInfomation");
            Sellers sellerAccountInfoToOBject = Newtonsoft.Json.JsonConvert.DeserializeObject<Sellers>(sellerAccountInfo);
            if (sellerAccountInfoToOBject.SRID != SRID)
            {
                return new NotFoundResult();
            }
            return new OkResult();
        }

        public async Task<IActionResult> CustomerIdentityCheckAsync(long CRID)
        {
            string customerAccountInfo = _httpContextAccessor.HttpContext.Session.GetString("CustomerInfomation");
            Customers customerAccountInfoToOBject = Newtonsoft.Json.JsonConvert.DeserializeObject<Customers>(customerAccountInfo);
            if (customerAccountInfoToOBject.CRID != CRID)
            {
                return new NotFoundResult();
            }
            return new OkResult();
        }        
        public async Task<IActionResult> AdminCheckAsync(short ADID)
        {
            string adminAccountInfo = _httpContextAccessor.HttpContext.Session.GetString("AdminInfomation");
            Admins adminAccountInfoToOBject = Newtonsoft.Json.JsonConvert.DeserializeObject<Admins>(adminAccountInfo);
            if (adminAccountInfoToOBject.ADID != ADID)
            {
                return new NotFoundResult();
            }
            return new OkResult();
        }
    }
}
