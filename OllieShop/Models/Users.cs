using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.Models;

public partial class Users
{
    [Display(Name="編號")]
    public long URID { get; set; }

    [Display(Name ="名稱")]
    [StringLength(50,ErrorMessage = "名稱不能超過50個字")]
    [Required(ErrorMessage = "必填欄位")]
    public string Name { get; set; } = null!;

    [Display(Name = "性別")]
    [Required(ErrorMessage ="必填欄位")]
    public bool Gender { get; set; }

    [Display(Name = "信箱")]
    [EmailAddress(ErrorMessage = "請填入有效的電子信箱")]
    [Required(ErrorMessage = "必填欄位")]
    public string Email { get; set; } = null!;

    [Display(Name = "生日")]
    [DisplayFormat (DataFormatString = "{0:yyyy/MM/dd}" )]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "必填欄位")]
    public DateTime BirthDay { get; set; }

    public virtual ICollection<Accounts> Accounts { get; set; } = new List<Accounts>();

    public virtual ICollection<Addresses> Addresses { get; set; } = new List<Addresses>();

    public virtual ICollection<Customers> Customers { get; set; } = new List<Customers>();

    public virtual ICollection<Sellers> Sellers { get; set; } = new List<Sellers>();

    public virtual ICollection<Violations> ViolationsSubmitterNavigation { get; set; } = new List<Violations>();

    public virtual ICollection<Violations> ViolationsSuspectNavigation { get; set; } = new List<Violations>();
}
