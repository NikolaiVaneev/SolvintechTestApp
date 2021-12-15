using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SolvintechTestApp.Data;
using SolvintechTestApp.Models;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvintechTestApp.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _db;



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(HomeViewModel model)
        {
            ApplicationUser user = new ApplicationUser { Email = model.RegistrationEmail, UserName = model.RegistrationEmail, Token = model.Token };

            var result = await _userManager.CreateAsync(user, model.RegistrationPassword);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                TempData[WC.Success] = "Registration completed successfully";
                return RedirectToAction("Index", "Account", user);
            }
            else
            {
                StringBuilder errorList = new();
                errorList.AppendLine("Registration error!");
                foreach (var error in result.Errors)
                {
                    errorList.AppendLine(error.Description);
                }
                TempData[WC.Warning] = errorList.ToString();
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(HomeViewModel model)
        {
            if (model == null)
            {
                return NotFound();
            }

            var user = _db.ApplicationUser.Where(u => u.UserName == model.LoginEmail).FirstOrDefault();

            if (user == null)
            {
                TempData[WC.Error] = "User not found. Check login";
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(model.LoginEmail, model.LoginPassword, false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    TempData[WC.Success] = $"Welcome, {user.UserName}";
                    return RedirectToAction("Index", "Account", user);
                }
                else
                {
                    TempData[WC.Warning] = "Password is not correct";
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public string TokenGenerate()
        {
            Guid token = Guid.NewGuid();
            return token.ToString();
        }

        [HttpGet]
        public IActionResult Index(ApplicationUser applicationUser)
        {
            return View(applicationUser);
        }
    }
}
