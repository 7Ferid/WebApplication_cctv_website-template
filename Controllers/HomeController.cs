using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using WebApplication_cctv_website_template.Context;
using WebApplication_cctv_website_template.ViewModels.TeamViewModels;


namespace WebApplication_cctv_website_template.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var teams = await _context.Teams.Select(x => new TeamGetVM()
            {
                Id = x.Id,
                Name = x.Name,
                ImagePath = x.ImagePath,
                PositionName = x.Position.Name
            }).ToListAsync();
            return View(teams);

        }
        
    }
}
