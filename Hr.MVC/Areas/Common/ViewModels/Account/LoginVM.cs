using System;

namespace Hr.MVC.Areas.Common.ViewModels.Account;

public class LoginVM
{
      public required string Email { get; set; }
      public required string Passord { get; set; }
      public bool RememberMe { get; set; }
}
