using System;
using System.ComponentModel.DataAnnotations;

namespace Hr.MVC.Areas.Common.ViewModels.Account;

public class RegisterVM
{
      [Required(ErrorMessage = "Name is required.")]
      [Length(3, 50, ErrorMessage ="Name must between 3-50 letter.")]
      public required string Name { get; set; }

      [Required(ErrorMessage = "Email is required.")]
      [EmailAddress]
      public required string Email { get; set; }

      [Required(ErrorMessage = "Password is required.")]
      [DataType(DataType.Password)]
      public required string Password { get; set; }

      [Required(ErrorMessage = "Confirm Password is required.")]
      [DataType(DataType.Password)]
      [Display(Name = "Confirm Password")]
      [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
      public required string ConfirmPassword { get; set; }
}
