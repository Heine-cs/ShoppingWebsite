using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.Models;

public partial class SellerPaymentMethods
{
    public long SRID { get; set; }

    public string PMID { get; set; } = null!;

    [Display(Name ="取消綁定")]
    public bool Canceled { get; set; }

    public virtual PaymentMethods PM { get; set; } = null!;

    public virtual Sellers SR { get; set; } = null!;
}
