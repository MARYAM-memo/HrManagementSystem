using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hr.MVC.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize]
    public class MyAttendanceController : Controller
    {
        [HttpGet]//عرض سجل الحضور والانصراف (مع فلترة بالشهر/السنة)
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]//تسجيل وقت الحضور
        public ActionResult CheckIn()
        {
            return View();
        }

        [HttpPost]//تسجيل وقت الانصراف
        public ActionResult CheckOut()
        {
            return View();
        }

        [HttpGet]//عرض حالة اليوم (دخلت ولا لسه)
        public ActionResult TodayStatus()
        {
            return View();
        }

        [HttpGet]//عرض ملخص حضور الشهر (عدد الأيام، التأخيرات)
        public ActionResult MonthlySummary()
        {
            return View();
        }

    }
}
