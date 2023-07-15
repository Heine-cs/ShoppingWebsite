using System;
using System.Collections.Generic;

namespace OllieShop.Models;

public partial class Accounts
{
    public long ACID { get; set; }

    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Level { get; set; } = null!;

    public long? URID { get; set; }

    public virtual Users? UR { get; set; }
}
