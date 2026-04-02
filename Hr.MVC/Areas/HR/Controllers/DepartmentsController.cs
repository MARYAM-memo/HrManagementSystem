using Hr.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hr.MVC.Areas.HR.Controllers
{
    [Area("HR")]
    [Authorize(Roles = $"{Constants.HrRole},{Constants.AdminRole}")]
    public class DepartmentsController : Controller
    {
        // GET: DepartmentsController
        public ActionResult Index()
        {
            return View();
        }

    }
}
