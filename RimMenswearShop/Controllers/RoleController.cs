using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RimMenswearShop.Models;
using RimMenswearShop.ViewModels.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.Controllers
{
    [Authorize(Roles = "Admin")]

    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var roles = roleManager.Roles;
            var model = new List<Role>();
            model = roles.Select(r => new Role()
            {
                RoleId = r.Id,
                NameRole = r.Name
            }).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await roleManager.CreateAsync(new IdentityRole()
                {
                    Name = model.Name
                });
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Role");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var model = new EditRoleViewModel()
                {
                    RoleId = role.Id,
                    NameRole = role.Name
                };
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.RoleId);
            if (role != null)
            {
                role.Name = model.NameRole;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Role");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View();
        }
        public async Task<IActionResult> Delete(string id)
        {
            var delRole = await roleManager.FindByIdAsync(id);
            if (delRole != null)
            {
                var result = await roleManager.DeleteAsync(delRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Role");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View();

        }
    }
}
