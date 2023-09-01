using baochi_test.Models;
using Microsoft.AspNetCore.Mvc;

namespace baochi_test.Components
{
    public class IsHotView : ViewComponent
    {
        private readonly BaochiTestContext _context;
        public IsHotView(BaochiTestContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.BaiDangs.ToList());
        }
    }
}
