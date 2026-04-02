using Hr.MVC.Areas.Employee.ViewModels.MyProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hr.MVC.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize]
    public class MyProfileController : Controller
    {
        [HttpGet]//عرض الملف الشخصي للموظف
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]//عرض نموذج تعديل البيانات الشخصية
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]//حفظ تعديلات البيانات الشخصية
        public ActionResult Edit(ProfileVM model)
        {
            return View();
        }

        [HttpGet]//عرض نموذج تغيير كلمة المرور
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]//تنفيذ تغيير كلمة المرور
        public ActionResult ChangePassword(ChangePasswordVM model)
        {
            return View();
        }

        [HttpPost]//رفع وتحديث الصورة الشخصية
        public ActionResult UpdateProfilePicture(UpdateProfilePictureVM model)
        {
            return View();
        }



    }
}
