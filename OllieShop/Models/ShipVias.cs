using System;
using System.Collections.Generic;

namespace OllieShop.Models;

public partial class ShipVias
{
    public string SVID { get; set; } = null!;

    public string Name { get; set; } = null!;

    public short? ADID { get; set; }

    public virtual Admins? AD { get; set; }

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}
