using System;
using System.Collections.Generic;

namespace OllieShop.Models;

public partial class CustomerCoupons
{
    public long CRCNID { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime? AppliedDate { get; set; }

    public long? CNID { get; set; }

    public long? CRID { get; set; }

    public virtual Coupons? CN { get; set; }

    public virtual Customers? CR { get; set; }
}
