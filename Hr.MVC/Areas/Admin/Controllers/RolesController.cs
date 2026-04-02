using Hr.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hr.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Constants.AdminRole)]
    public class RolesController : Controller
    {
        // GET: RolesController
        public ActionResult Index()
        {
            return View();
        }

    }
}
