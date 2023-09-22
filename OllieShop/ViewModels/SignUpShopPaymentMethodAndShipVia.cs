using OllieShop.Models;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.ViewModels
{
    public class SignUpShopPaymentMethodAndShipVia
    {
        [Display(Name = "運送方式")]
        public SellerShipVias sellerShipVia { get; set; }

        [Display(Name = "付款方式")]
        public SellerPaymentMethods sellerPaymentMethod { get; set; }
    }
}
