using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.Models;

public partial class Announcements
{
    [Display(Name="編號")]
    public int ATID { get; set; }

    [Display(Name = "發布日")]
    [Required(ErrorMessage = "必填欄位")]
    public DateTime PublicDate { get; set; }

    [Display(Name = "內容")]
    [DataType(DataType.MultilineText)]
    [Required(ErrorMessage = "必填欄位")]
    public string ATContent { get; set; } = null!;

    public short? ADID { get; set; }

    [Display(Name = "管理員編號")]
    public virtual Admins? AD { get; set; }

    [Display(Name = "標題")]
    [Required(ErrorMessage = "必填欄位")]
    public string Title { get; set; } = null!;
}
