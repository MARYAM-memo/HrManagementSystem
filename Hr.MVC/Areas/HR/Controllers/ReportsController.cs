using Hr.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hr.MVC.Areas.HR.Controllers
{
    [Area("HR")]
    [Authorize(Roles = $"{Constants.HrRole},{Constants.AdminRole}")]
    public class ReportsController : Controller
    {
        // GET: ReportsController
        public ActionResult Index()
        {
            return View();
        }

    }
}
