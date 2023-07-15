using System;
using System.Collections.Generic;

namespace OllieShop.Models;

public partial class PaymentMethods
{
    public string PMID { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();

    public virtual ICollection<SellerPaymentMethods> SellerPaymentMethods { get; set; } = new List<SellerPaymentMethods>();
}
