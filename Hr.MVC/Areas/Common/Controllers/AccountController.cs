using Hr.Infrastructure.Data.IdentityEntities;
using Hr.MVC.Areas.Common.ViewModels.Account;
using Hr.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Hr.MVC.Areas.Common.Controllers
{
    [Area("Common")]
    public class AccountController(UserManager<ApplicationUser> userMng, SignInManager<ApplicationUser> signInMng, RoleManager<ApplicationRole> roleMng) : Controller
    {
        readonly UserManager<ApplicationUser> userManager = userMng;
        readonly SignInManager<ApplicationUser> signInManager = signInMng;
        readonly RoleManager<ApplicationRole> roleManager = roleMng;


        [AllowAnonymous]
        public ActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM model,string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Passord, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, Constants.LoginError);
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    FullName = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    IsActive = true,
                    CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (!await roleManager.RoleExistsAsync(Constants.UserRole))
                    {
                        await roleManager.CreateAsync(new ApplicationRole { Name = Constants.UserRole, CreatedAt = DateOnly.FromDateTime(DateTime.Now) });
                    }
                    
                    await userManager.AddToRoleAsync(user, Constants.UserRole);
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home", new { area = "Common" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
