using Hr.MVC.Areas.Employee.ViewModels.MyLeaves;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hr.MVC.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize]
    public class MyLeavesController : Controller
    {
        [HttpGet]//عرض جميع طلبات الإجازات السابقة
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]//عرض نموذج طلب إجازة جديد
        public new ActionResult Request()
        {
            return View();
        }

        [HttpPost]//تقديم طلب إجازة جديد
        public new ActionResult Request(RequestVM model)
        {
            return View();
        }

        [HttpGet]//عرض تفاصيل إجازة معينة
        public ActionResult Details()
        {
            return View();
        }

        [HttpPost]//إلغاء طلب إجازة (فقط لو حالته Pending)
        public ActionResult Cancel()
        {
            return View();
        }

        [HttpGet]//عرض رصيد الإجازات المتبقي
        public ActionResult Balance()
        {
            return View();
        }
    }
}
