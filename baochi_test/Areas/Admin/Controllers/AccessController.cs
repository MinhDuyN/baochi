using Microsoft.AspNetCore.Mvc;
using baochi_test.Models;

namespace baochi_test.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccessController : Controller
    {
        BaochiTestContext db = new BaochiTestContext();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Login(TaiKhoan user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.TaiKhoans.Where(x => x.TaiKhoan1.Equals(user.TaiKhoan1) &&
                x.MatKhau.Equals(user.MatKhau)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.TaiKhoan1.ToString());
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }

            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }
    }
}
