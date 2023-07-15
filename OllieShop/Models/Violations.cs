using System;
using System.Collections.Generic;

namespace OllieShop.Models;

public partial class Violations
{
    public long VioID { get; set; }

    public long? Submitter { get; set; }

    public long? Suspect { get; set; }

    public string Reason { get; set; } = null!;

    public DateTime SubmitDate { get; set; }

    public bool? Disabled { get; set; }

    public short? ADID { get; set; }

    public virtual Admins? AD { get; set; }

    public virtual Users? SubmitterNavigation { get; set; }

    public virtual Users? SuspectNavigation { get; set; }
}
