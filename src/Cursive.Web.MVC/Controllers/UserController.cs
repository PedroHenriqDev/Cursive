using Microsoft.AspNetCore.Mvc;

namespace Cursive.Web.MVC.Controllers;

public class UserController : Controller
{
    [HttpGet]
    public IActionResult Login() => View();
}
