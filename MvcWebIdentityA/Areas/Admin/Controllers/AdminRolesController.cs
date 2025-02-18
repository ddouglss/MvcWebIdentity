﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcWebIdentityA.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;

namespace MvcWebIdentityA.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminRolesController : Controller
{
    private RoleManager<IdentityRole> roleManager;
    private UserManager<IdentityUser> userManager;

    public AdminRolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        this.roleManager = roleManager;
        this.userManager = userManager;
    }

    public ViewResult Index() => View(roleManager.Roles); //Exebição de todas as Roles

    //Criação de uma Roles
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create([Required] string name)
    {
        if (ModelState.IsValid)
        {
            IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                Errors(result);
        }
        return View(name);
    }

    [HttpGet]
    //Edição de uma Roles
    public async Task<IActionResult> Update(string id)
    {
        IdentityRole role = await roleManager.FindByIdAsync(id);

        // Materializa a lista para evitar múltiplas conexões abertas
        var users = await userManager.Users.ToListAsync();

        List<IdentityUser> members = new List<IdentityUser>();
        List<IdentityUser> nonMembers = new List<IdentityUser>();

        foreach (IdentityUser user in users)
        {
            var list = await userManager.IsInRoleAsync(user, role.Name)
                                     ? members : nonMembers;
            list.Add(user);
        }

        return View(new RoleEdit
        {
            Role = role,
            Members = members,
            NonMembers = nonMembers
        });
    }


    [HttpPost]
    public async Task<IActionResult> Update(RoleModification model)
    {
        IdentityResult result;

        if (ModelState.IsValid)
        {
            foreach (string userId in model.AddIds ?? new string[] { })
            {
                IdentityUser user = await userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    result = await userManager.AddToRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                        Errors(result);
                }
            }
            foreach (string userId in model.DeleteIds ?? new string[] { })
            {
                IdentityUser user = await userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    result = await userManager.RemoveFromRoleAsync(user, model.RoleName);

                    if (!result.Succeeded)
                        Errors(result);
                }
            }
        }
        if (ModelState.IsValid)
            return RedirectToAction(nameof(Index));
        else
            return await Update(model.RoleId);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        IdentityRole role = await roleManager.FindByIdAsync(id);

        if (role == null)
        {
            ModelState.AddModelError("", "Role não encontrada");
            return View("Index", roleManager.Roles);
        }

        return View(role);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var role = await roleManager.FindByIdAsync(id);

        if (role != null)
        {
            IdentityResult result = await roleManager.DeleteAsync(role);

            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                Errors(result);
        }
        else
        {
            ModelState.AddModelError("", "Role não encontrada");
        }
        return View("Index", roleManager.Roles);
    }


    private void Errors(IdentityResult result)
    {
        foreach (IdentityError error in result.Errors)
            ModelState.AddModelError("", error.Description);
    }
}
