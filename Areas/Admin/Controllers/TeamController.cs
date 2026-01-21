using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication_cctv_website_template.Context;
using WebApplication_cctv_website_template.Helpers;
using WebApplication_cctv_website_template.Models;
using WebApplication_cctv_website_template.ViewModels.TeamViewModels;

namespace WebApplication_cctv_website_template.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles ="Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environnment;
        private readonly string _folderpath;

        public TeamController(AppDbContext context, IWebHostEnvironment environnment)
        {
            _context = context;
            _environnment = environnment;
            _folderpath = Path.Combine(_environnment.WebRootPath, "img");
        }

        public async Task<IActionResult> Index()
        {
            var teams = await _context.Teams.Select(x=> new TeamGetVM()
            {
                Id=x.Id,
                Name=x.Name,
                ImagePath=x.ImagePath,
                PositionName=x.Position.Name
            }).ToListAsync();
            return View(teams);
        }

        public async Task<IActionResult> Create()
        {
            await _sendPositionInViewbag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeamCreateVM vm)
        {
            await _sendPositionInViewbag();
            if (!ModelState.IsValid)
                return View(vm);
            var isExistPosition = await _context.Postitions.AnyAsync(x => x.Id == vm.PositionId);
            if (!isExistPosition)
            {
                ModelState.AddModelError("PositionId", "Bele position movcud deyil");
                return View(vm);
            }
            if (!vm.Image.CheckSize(2))
            {
                ModelState.AddModelError("Image", "2 mb dan cox olmamalidir");
                return View(vm);
            }
            if (!vm.Image.CheckType("image"))
            {
                ModelState.AddModelError("Image", "yalniz image yuklemeke olar");
                return View(vm);
            }

            var uniqueFileName = await vm.Image.FileUpload(_folderpath);

            Team team = new()
            {
                Name=vm.Name,
                PositionId=vm.PositionId,
                ImagePath=uniqueFileName    
            };

            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
           return RedirectToAction("Index");


            

        }

        public async Task<IActionResult> Delete(int id)
        {
            var findId = await _context.Teams.FindAsync(id);
            if (findId is null)
                return NotFound();

            _context.Teams.Remove(findId);
            await _context.SaveChangesAsync();
            var deletedImagePath = Path.Combine(_folderpath, findId.ImagePath);

            FileHelper.FileDelete(deletedImagePath);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Update(int id)
        {
            var product = await _context.Teams.FindAsync(id);
            if (product is null)
                return NotFound();

            TeamUpdateVM vm = new()
            {
                Id = product.Id,
                PositionId = product.PositionId,
                Name = product.Name,
              
            };

            await _sendPositionInViewbag();
            return View(vm);
        }

        [HttpPost]

        public async Task<IActionResult> Update(TeamUpdateVM vm)
        {
            await _sendPositionInViewbag();


            if (!ModelState.IsValid)
                return View(vm);


            var isExistcategory = await _context.Postitions.AnyAsync(x => x.Id == vm.PositionId);
            if (!isExistcategory)
            {
                ModelState.AddModelError("CategoryId", "This category is not found");
                return View(vm);
            }
            if (!vm.Image?.CheckSize(2) ?? false)
            {
                ModelState.AddModelError("Image", "Image's maximum size must be 2 mb");
                return View(vm);
            }
            if (!vm.Image?.CheckType("image") ?? false)
            {
                ModelState.AddModelError("Image", "You can upload file in only image format ");
                return View(vm);
            }

            var existProduct = await _context.Teams.FindAsync(vm.Id);
            if (existProduct is null)
                return BadRequest();



            existProduct.Name = vm.Name;
            existProduct.PositionId = vm.PositionId;
        

            if (vm.Image is { })
            {
                string newImagePath = await vm.Image.FileUpload(_folderpath);
                string oldImagePath = Path.Combine(_folderpath, existProduct.ImagePath);
                FileHelper.FileDelete(oldImagePath);
                existProduct.ImagePath = newImagePath;
            }

            _context.Teams.Update(existProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }



        private async Task _sendPositionInViewbag()
        {
            var positions = await _context.Postitions.Select(x=> new SelectListItem()
            {
                Value=x.Id.ToString(),
                Text=x.Name
            }).ToListAsync();
            ViewBag.Positions = positions;
        }
    }
}
