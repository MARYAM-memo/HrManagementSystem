using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hr.MVC.Areas.Common.Controllers
{
    [Area("Common")]
    [AllowAnonymous]
    public class ErrorsController : Controller
    {
        [AllowAnonymous]
        public ActionResult AccessDenied()
        {
            return View();
        }

        public new IActionResult NotFound()
        {
            return View();
        }

        public IActionResult InternalServerError()
        {
            return View();
        }

        public IActionResult Error(int code)
        {
            ViewBag.ErrorCode = code;
            return View();
        }

    }
}
