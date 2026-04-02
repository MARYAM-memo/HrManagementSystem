using Microsoft.AspNetCore.Mvc;

namespace Hr.MVC.Areas.Employee.Controllers
{
    public class DashboardController : Controller
    {
        // GET: DashboardController
        public ActionResult Index()
        {
            return View();
        }

    }
}
