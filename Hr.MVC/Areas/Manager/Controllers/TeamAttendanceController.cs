using Hr.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hr.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = $"{Constants.HrRole},{Constants.AdminRole},{Constants.ManagerRole}")]
    public class TeamAttendanceController : Controller
    {
        // GET: TeamAttendanceController
        public ActionResult Index()
        {
            return View();
        }

    }
}
