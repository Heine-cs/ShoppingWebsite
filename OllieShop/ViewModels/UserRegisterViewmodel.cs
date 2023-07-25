using OllieShop.Models;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.ViewModels
{
    public class UserRegisterViewmodel
    {
            //USER
            [Display(Name = "編號")]
            public long URID { get; set; }

            [Display(Name = "名稱")]
            [StringLength(50, ErrorMessage = "名稱不能超過50個字")]
            [Required(ErrorMessage = "必填欄位")]
            public string Name { get; set; } = null!;

            [Display(Name = "性別")]
            [Required(ErrorMessage = "必填欄位")]
            public bool Gender { get; set; }

            [Display(Name = "信箱")]
            [EmailAddress(ErrorMessage = "請填入有效的電子信箱")]
            [Required(ErrorMessage = "必填欄位")]
            public string Email { get; set; } = null!;

            [Display(Name = "生日")]
            [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
            [DataType(DataType.Date)]
            [Required(ErrorMessage = "必填欄位")]
            public DateTime BirthDay { get; set; }


            //Account
            public long ACID { get; set; }

            [Display(Name = "帳號")]
            [Required(ErrorMessage = "必填欄位")]
            [StringLength(20,ErrorMessage = "帳號長度不得超過20字")]
            [RegularExpression("[^\u4e00-\u9fa5]{0,20}", ErrorMessage = "僅接受輸入純英文或純數字或英數混合的帳號")]
            public string Account { get; set; } = null!;

            [Display(Name = "密碼")]
            [Required(ErrorMessage = "必填欄位")]
            [StringLength(20, ErrorMessage = "密碼長度不得超過20字")]
            [RegularExpression("[^\u4e00-\u9fa5]{0,20}", ErrorMessage = "僅接受輸入純英文或純數字或英數混合的密碼")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = null!;

            [Display(Name = "密碼確認")]
            [Required(ErrorMessage = "必填欄位")]
            [Compare("Password",ErrorMessage ="須與密碼欄位相同")]
            [DataType(DataType.Password)]
            public string PasswordConfirm { get; set; } = null!;

            [Display(Name = "階級")]
            public string Level { get; set; } = null!;


            //Address
            public long ASID { get; set; }

            [Display(Name = "區")]
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
            [StringLength(10, ErrorMessage = "欄位不能超過10個字")]
            [Required(ErrorMessage = "必填欄位")]
            public string Phone { get; set; } = null!;

    }
}
