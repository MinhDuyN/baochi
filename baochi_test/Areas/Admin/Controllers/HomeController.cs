using baochi_test.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace baochi_test.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [Authentication]
        public IActionResult Index()
        {
            return View();
        }
    }
}
