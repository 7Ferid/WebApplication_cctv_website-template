using Microsoft.AspNetCore.Mvc;

namespace WebApplication_cctv_website_template.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
