using System;
using System.Collections.Generic;

namespace OllieShop.Models;

public partial class OrderDetails
{
    public long ORID { get; set; }

    public long PTID { get; set; }

    public long SNID { get; set; }

    public int Quantity { get; set; }

    public virtual Orders OR { get; set; } = null!;

    public virtual Products PT { get; set; } = null!;

    public virtual Specifications SN { get; set; } = null!;
}
