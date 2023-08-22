using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.Models;

public partial class Addresses
{
    public long ASID { get; set; }

    [Display(Name ="區")]
    [StringLength(10, ErrorMessage = "欄位不能超過10個字")]
    [Required(ErrorMessage = "必填欄位")]
    public string District { get; set; } = null!;

    [Display(Name = "詳細地址")]
    [StringLength(30, ErrorMessage = "欄位不能超過30個字")]
    [Required(ErrorMessage = "必填欄位")]
    public string Street { get; set; } = null!;

    [Display(Name = "市/縣")]
    [StringLength(10, ErrorMessage = "欄位不能超過10個字")]
    [Required(ErrorMessage = "必填欄位")]
    public string City { get; set; } = null!;

    [Display(Name = "手機號碼")]
    [StringLength(10, ErrorMessage = "欄位不能超過24個字")]
    [Required(ErrorMessage = "必填欄位")]
    public string Phone { get; set; } = null!;

    public long? URID { get; set; }

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();

    public virtual Users UR { get; set; }
}
