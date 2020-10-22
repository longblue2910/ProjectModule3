using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RimMenswearShop.Models.UserModel;
using RimMenswearShop.ViewModels;
using System.Threading.Tasks;

namespace RimMenswearShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, 
                                 SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        //public IActionResult Index()
        //{
        //    var users = userManager.Users;
        //    var model = new List<IdentityUser>();
        //    model = users.Select(u => new IdentityUser()
        //    {
        //        Id = u.Id,
        //        UserName = u.UserName
        //    }).ToList();
        //    return View(model);
        //}
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                 {
                    UserName = model.Email,
                    Email = model.Email
                };
            var result = await userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user : user, isPersistent : false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                              userName : model.Email,
                              password : model.Password,
                              isPersistent: model.RememberMe,
                              lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attemp");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            if (!signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userManager.ChangePasswordAsync(userManager.FindByNameAsync(User.Identity.Name).Result, model.Password, model.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }
    }
}
