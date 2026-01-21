using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication_cctv_website_template.Models;
using WebApplication_cctv_website_template.ViewModels.UserViewModels;

namespace WebApplication_cctv_website_template.Controllers
{
    public class AccountController(UserManager<AppUser> _userManager , SignInManager<AppUser> _signInManager, RoleManager<IdentityRole> _roleManager ) : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            AppUser user = new()
            {
                FullName=vm.FullName,
                UserName=vm.UserName,
                Email=vm.Email
            };

            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vm);
            }

           /* await _roleManager.AddToRoleAsync*/

            await _signInManager.SignInAsync(user, false);
           return RedirectToAction("Index", "Home");

        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var User = await _userManager.FindByEmailAsync(vm.Email);
            if(User is null)
            {
                ModelState.AddModelError("", "Password or Email is wrong");
                return View(vm);
            }

            var result = await _signInManager.PasswordSignInAsync(User, vm.Password, false, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Password or Email is wrong");
                return View(vm);
            }

            return RedirectToAction("Index", "Home");


        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> CreateRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Member"

            });
            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Admin"

            });
            return Ok("OK!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }

    }
}
