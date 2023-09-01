using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.Models;

public partial class Accounts
{
    public long ACID { get; set; }

    [Display(Name = "帳號")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(20,ErrorMessage ="帳號不得超過20個字")]
    public string Account { get; set; } = null!;

    [Display(Name = "密碼")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(20, ErrorMessage = "帳號不得超過20個字")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    //呼叫方法後輸入字串參數就可以替換整段字串
    public string GetMaskedPassword(string unDealPassword)
    {
        // Convert the real password to *** representation
        return new string('*', unDealPassword.Length);
    }

    [Display(Name = "階級")]
    [StringLength(1)]
    public string Level { get; set; } = null!;

    public long? URID { get; set; }

    [Display(Name = "用戶編號")]
    //avoid Self referencing loop detected for property
    [JsonIgnore]
    public virtual Users UR { get; set; }
}
