using Microsoft.AspNetCore.Mvc;

namespace tuoitrevanduc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        [Route("/admin/khoa-tu")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/admin/loai-khoa-tu")]
        public IActionResult CategoryIndex()
        {
            return View();
        }
    }
}
