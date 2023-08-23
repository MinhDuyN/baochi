using baochi_test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System.Diagnostics;

namespace baochi_test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BaochiTestContext _context;
        public HomeController(ILogger<HomeController> logger, BaochiTestContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 1;
            var lstsanpham = _context.BaiDangs.AsNoTracking().OrderBy(x => x.Ten);
            IPagedList<BaiDang> models = new PagedList<BaiDang>(lstsanpham, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        public IActionResult PhanLoai(int maloai, int? page)
        {
            //Cách dài
            var layIdDM = _context.DanhMucs.AsNoTracking().Where(x => x.Id == maloai);
            var baiDangs = _context.BaiDangs.Where(b => b.IdDanhMuc == maloai);


            var pageNumber = page == null || page < 0 ? 1 : page.Value;
            var pageSize = 1;

            //Cách ngắn hơn
            //var baiDangs = _context.BaiDangs
            //                      .Where(b => b.IdDanhMuc == maloai)
            //                      .OrderBy(x => x.Ten);

            IPagedList<BaiDang> models = new PagedList<BaiDang>(baiDangs, pageNumber, pageSize);
            //Lấy viewbag giữ maloai đem qua view so sánh
            ViewBag.maloai = maloai;
            return View(models);

        }

        public IActionResult ChiTiet(int id)
        {
            var chitiet = _context.BaiDangs.Where(x=>x.Id == id).ToList();
            return View(chitiet);

        }

        public IActionResult TimKiem(string tukhoa) {

            //Lưu biến bên View
            ViewData["bientimkiem"] = tukhoa;
            //Lấy tất cả dữ liệu trong BaiDangs
            var bienluutru = from b in _context.BaiDangs select b;
            if (!String.IsNullOrEmpty(tukhoa)) //Kiểm tra xem có tồn tại trong dtb
            {
                bienluutru = bienluutru.Where(b => b.Ten.Contains(tukhoa));
            }
            return View(bienluutru);

            //Cách 2
            //var query = _context.BaiDangs.AsQueryable();

            //if (!string.IsNullOrEmpty(timkiem))
            //{
            //    query = query.Where(b => b.Ten.Contains(timkiem));
            //}

            //ViewData["bientimkiem"] = timkiem;
            //return View(query.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}