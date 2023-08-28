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

    }
}
