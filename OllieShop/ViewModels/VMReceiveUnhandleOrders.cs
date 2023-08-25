using Microsoft.AspNetCore.Mvc.Rendering;
using OllieShop.Models;

namespace OllieShop.ViewModels
{
    public class VMReceiveUnhandleOrders
    {
        public Products[] products { get; set; }

        public Specifications[] specifications { get; set; }

        public Orders[] orders { get; set; }

        //Store Product requireQuantities for shopping cart page
        public int[] RequireQuantities { get; set; }

        //Store PaymentMethod&ShipVias dropdownlist for bill page
        public string[] sellerPaymentMethodOptions { get; set; }
        public string[] sellerShipViaOptions { get; set; }
        public SelectListItem[] customerPaymentCardOptions { get; set; }
    }
}
