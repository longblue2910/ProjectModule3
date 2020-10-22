using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RimMenswearShop.Models.UserModel;
using RimMenswearShop.ViewModels.User;

namespace RimMenswearShop.Controllers
{
    [Authorize(Roles = "Admin")]

    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
                               RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var users = userManager.Users;
            if (users != null && users.Any())
            {
                var model = new List<UserViewModel>();
                model = users.Select(u => new UserViewModel()
                {
                    UserId = u.Id,
                    Address = u.Address,
                    City = u.City,
                    Email = u.Email,
                    Phone = u.PhoneNumber
                }).ToList();
                foreach (var user in model)
                {
                    user.RoleName = GetRolesName(user.UserId);
                }
                return View(model);
            }
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = roleManager.Roles;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    City = model.City,
                    Address = model.Address,
                    PhoneNumber = model.Phone
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.RolesID))
                    {
                        var role = await roleManager.FindByIdAsync(model.RolesID);
                        var addRoleResult = await userManager.AddToRoleAsync(user, role.Name);
                        if (addRoleResult.Succeeded)
                        {
                            return RedirectToAction("Index", "User");
                        }
                        foreach (var error in addRoleResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
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
        public string GetRolesName(string userId)
        {
            var user = Task.Run(async () => await userManager.FindByIdAsync(userId)).Result;
            var roles = Task.Run(async () => await userManager.GetRolesAsync(user)).Result;
            return roles != null ? string.Join(", ", roles) : string.Empty;
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var model = new EditUserViewModel
                {
                    Address = user.Address,
                    UserId = user.Id,
                    Email = user.Email,
                    Phone = user.PhoneNumber
                };
                var rolesName = await userManager.GetRolesAsync(user);
                if (rolesName != null && rolesName.Any())
                {
                    var role = await roleManager.FindByNameAsync(rolesName.FirstOrDefault());
                    model.RolesID = role.Id;
                }

                ViewBag.Roles = roleManager.Roles;
                return View(model);
            }

            return View("~/Views/Error/PageNotFound.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    user.Address = model.Address;
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.PhoneNumber = model.Phone;
                    user.Address = model.Address;
                    user.City = model.City;
                    user.Id = model.UserId;                
                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        var rolesName = await userManager.GetRolesAsync(user);
                        var delRoles = await userManager.RemoveFromRolesAsync(user, rolesName);

                        if (!string.IsNullOrEmpty(model.RolesID))
                        {
                            var role = await roleManager.FindByIdAsync(model.RolesID);
                            var addRoleResult = await userManager.AddToRoleAsync(user, role.Name);
                            if (addRoleResult.Succeeded) return RedirectToAction("Index", "User");
                            foreach (var error in addRoleResult.Errors) ModelState.AddModelError("", error.Description);
                        }

                        return RedirectToAction("Index", "User");
                    }

                    foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }
        public async Task<IActionResult> Remove(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded) return RedirectToAction("Index", "User");
                foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            }

            return View();
        }
    }
}
