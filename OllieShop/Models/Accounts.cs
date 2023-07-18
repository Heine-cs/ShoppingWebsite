using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.Models;

public partial class Accounts
{
    public long ACID { get; set; }

    [Display(Name = "帳號")]
    public string Account { get; set; } = null!;

    [Display(Name = "密碼")]
    public string Password { get; set; } = null!;
    //呼叫方法後輸入字串參數就可以替換整段字串
    public string GetMaskedPassword(string unDealPassword)
    {
        // Convert the real password to *** representation
        return new string('*', unDealPassword.Length);
    }

    [Display(Name = "階級")]
    public string Level { get; set; } = null!;

    public long? URID { get; set; }

    [Display(Name = "用戶編號")]
    public virtual Users? UR { get; set; }
}
