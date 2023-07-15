using System;
using System.Collections.Generic;

namespace OllieShop.Models;

public partial class Sellers
{
    public long SRID { get; set; }

    public string ShopNAME { get; set; } = null!;

    public string TaxID { get; set; } = null!;

    public string BankCode { get; set; } = null!;

    public string BankAccount { get; set; } = null!;

    public long? URID { get; set; }

    public virtual ICollection<Messages> Messages { get; set; } = new List<Messages>();

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();

    public virtual ICollection<Products> Products { get; set; } = new List<Products>();

    public virtual ICollection<SellerPaymentMethods> SellerPaymentMethods { get; set; } = new List<SellerPaymentMethods>();

    public virtual Users? UR { get; set; }
}
