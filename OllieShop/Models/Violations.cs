using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.Models;

public partial class Violations
{
    [Display(Name = "案號")]
    public long VioID { get; set; }

    [Display(Name = "提交用戶編號")]
    public long? Submitter { get; set; }

    [Display(Name = "受審理用戶編號")]
    public long? Suspect { get; set; }

    [Display(Name = "提交原因")]
    public string Reason { get; set; } = null!;

    [Display(Name = "提交日")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh點:mm分:ss秒}")]
    [DataType(DataType.DateTime)]
    public DateTime SubmitDate { get; set; }

    [Display(Name = "是否停權")]
    public bool? Disabled { get; set; }

    public short? ADID { get; set; }

    [Display(Name = "管理員帳號")]
    public virtual Admins? AD { get; set; }

    public virtual Users? SubmitterNavigation { get; set; }

    public virtual Users? SuspectNavigation { get; set; }
}
