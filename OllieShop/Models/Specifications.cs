using System;
using System.Collections.Generic;

namespace OllieShop.Models;

public partial class Specifications
{
    public long SNID { get; set; }

    public string Name { get; set; } = null!;

    public string Picture { get; set; } = null!;

    public double Weight { get; set; }

    public double Size { get; set; }

    public int LeadDay { get; set; }

    public double PackageSize { get; set; }

    public string Freebie { get; set; } = null!;

    public long? PTID { get; set; }

    public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();

    public virtual Products? PT { get; set; }
}
