using System;
using System.Collections.Generic;

namespace OllieShop.Models;

public partial class SellerPaymentMethods
{
    public long SRID { get; set; }

    public string PMID { get; set; } = null!;

    public bool Canceled { get; set; }

    public virtual PaymentMethods PM { get; set; } = null!;

    public virtual Sellers SR { get; set; } = null!;
}
