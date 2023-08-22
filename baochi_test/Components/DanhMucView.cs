using baochi_test.Models;
using Microsoft.AspNetCore.Mvc;

namespace baochi_test.Components
{
    public class DanhMucView : ViewComponent
    {
        private readonly BaochiTestContext _context;
        public DanhMucView(BaochiTestContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.DanhMucs.ToList());
        }
    }
}
