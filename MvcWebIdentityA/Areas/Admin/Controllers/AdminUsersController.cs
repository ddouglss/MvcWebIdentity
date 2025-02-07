﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MvcWebIdentityA.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminUsersController : Controller
{
    private readonly UserManager<IdentityUser> userManager;

    public AdminUsersController(UserManager<IdentityUser> userManager)
    {
        this.userManager = userManager;
    }

    [HttpGet]

    public IActionResult Index()
    {
        var users = userManager.Users.ToList();
        return View(users);
    }

    [HttpPost]

    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound($"Usuário com Id = {id} não foi encontrado.");
        }

        else
        {
            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            // Em caso de erro, recarregamos a lista de usuários para evitar erro de View
            var users = await userManager.Users.ToListAsync();
            return View("Index");
        }
    }
}