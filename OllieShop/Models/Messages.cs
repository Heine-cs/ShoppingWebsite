using System;
using System.Collections.Generic;

namespace OllieShop.Models;

public partial class Messages
{
    public long MEID { get; set; }

    public DateTime PostDate { get; set; }

    public string MEContent { get; set; } = null!;

    public long? CRID { get; set; }

    public long? SRID { get; set; }

    public virtual Customers? CR { get; set; }

    public virtual Sellers? SR { get; set; }
}
