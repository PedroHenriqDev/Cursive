using System.Web.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cursive.Web.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
