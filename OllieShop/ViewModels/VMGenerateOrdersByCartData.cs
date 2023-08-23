using Microsoft.AspNetCore.Mvc.Rendering;
using OllieShop.Models;

namespace OllieShop.ViewModels
{
    public class VMGenerateOrdersByCartData
    {
        public Products products { get; set; }

        public Specifications specifications { get; set; }

        public Orders orders { get; set; }

        //Store Product requireQuantities for shopping cart page
        public int RequireQuantities { get; set; }

        //Store PaymentMethod&ShipVias dropdownlist for bill page
        public List<SelectListItem> sellerPaymentMethodOptions { get; set; }
        public List<SelectListItem> sellerShipViaOptions { get; set; }
        public List<SelectListItem> customerPaymentCardOptions { get; set; }

    }
}
