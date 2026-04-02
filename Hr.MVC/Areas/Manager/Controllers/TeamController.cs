using Hr.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hr.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = $"{Constants.HrRole},{Constants.AdminRole},{Constants.ManagerRole}")]
    public class TeamController : Controller
    {
        // GET: TeamController
        public ActionResult Index()
        {
            return View();
        }

    }
}
