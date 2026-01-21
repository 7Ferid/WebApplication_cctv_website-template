using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication_cctv_website_template.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();

        }
        
    }
}
