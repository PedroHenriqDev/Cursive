using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cursive.Web.MVC.Controllers;

[Authorize(AuthenticationSchemes = "CookieAuth")]
public class TestController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
    