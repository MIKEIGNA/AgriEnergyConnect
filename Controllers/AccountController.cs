using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AgriEnergyConnect.Models;
using AgriEnergyConnect.ViewModels;
using System.Threading.Tasks;

namespace AgriEnergyConnect.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        if (model.Email == "employee@example.com")
                        {
                            return RedirectToAction("Index", "Employee");
                        }
                        else if (await _userManager.IsInRoleAsync(user, "Farmer"))
                        {
                            return RedirectToAction("Index", "Farmer");
                        }
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                ViewData["InvalidLoginAttempt"] = "Invalid email or password.";
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
