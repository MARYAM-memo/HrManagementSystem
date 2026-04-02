using Hr.Infrastructure.Data.IdentityEntities;
using Hr.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hr.MVC.Areas.Common.Controllers
{
    [Area("Common")]
    [Authorize]
    public class HomeController(UserManager<ApplicationUser> userMng) : Controller
    {
        readonly UserManager<ApplicationUser> userManager = userMng;

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("NotFound", "Errors");
            var roles = await userManager.GetRolesAsync(user);

            if (roles.Contains(Constants.AdminRole))
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            else if (roles.Contains(Constants.HrRole))
                return RedirectToAction("Index", "Dashboard", new { area = "HR" });
            else if (roles.Contains(Constants.ManagerRole))
                return RedirectToAction("Index", "Dashboard", new { area = "Manager" });
            else if (roles.Contains(Constants.EmployeeRole))
                return RedirectToAction("Index", "Dashboard", new { area = "Employee" });
            else return RedirectToAction("Welcome");

        }

        public IActionResult Welcome()
        {
            return View();
        }

        // صفحة عن النظام
        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

        // صفحة المساعدة
        [AllowAnonymous]
        public IActionResult Help()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

    }
}
