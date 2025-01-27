using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MvcWebIdentityA.Areas.Admin.Controllers;

//especificar que esse controlador pertence a Area "Admin"
[Area("Admin")]
[Authorize(Roles = "Admin")] //Assim não tera como acessar a pela URL
public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
