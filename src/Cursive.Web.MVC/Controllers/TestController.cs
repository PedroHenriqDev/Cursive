using Microsoft.AspNetCore.Mvc;

namespace Cursive.Web.MVC.Controllers;

public class TestController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
