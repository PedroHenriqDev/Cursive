using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cursive.Web.MVC.Controllers;

[Authorize]
public class DocumentController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
