using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hr.MVC.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize]
    public class MyPayrollController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Print()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Latest()
        {
            return View();
        }

    }
}
