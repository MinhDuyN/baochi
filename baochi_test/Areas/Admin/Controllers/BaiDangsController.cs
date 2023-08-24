using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using baochi_test.Models;
using PagedList.Core;

namespace baochi_test.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BaiDangsController : Controller
    {
        private readonly BaochiTestContext _context;

        public BaiDangsController(BaochiTestContext context)
        {
            _context = context;
        }

        // GET: Admin/BaiDangs
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 4;

            var baochiTestContext = _context.BaiDangs.Include(b => b.IdDanhMucNavigation);

            IPagedList<BaiDang> models = new PagedList<BaiDang>(baochiTestContext, pageNumber, pageSize);
            return View(models);
        }

        // GET: Admin/BaiDangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BaiDangs == null)
            {
                return NotFound();
            }

            var baiDang = await _context.BaiDangs
                .Include(b => b.IdDanhMucNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baiDang == null)
            {
                return NotFound();
            }

            return View(baiDang);
        }

        // GET: Admin/BaiDangs/Create
        public IActionResult Create()
        {
            ViewData["IdDanhMuc"] = new SelectList(_context.DanhMucs, "Id", "Ten");
            return View();
        }

        // POST: Admin/BaiDangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,HinhAnh,NoiDung,Active,IdDanhMuc")] BaiDang baiDang, IFormFile HinhAnh)
        {
            if (ModelState.IsValid)
            {
                //Lấy tên ảnh gốc
                var fileName = Path.GetFileNameWithoutExtension(HinhAnh.FileName);
                //Lấy đuôi chấm
                var fileExtension = Path.GetExtension(HinhAnh.FileName);
                //Tạo tên
                var tenValue = _context.Entry(baiDang).Property("Ten").CurrentValue.ToString();
                var uniqueFileName = tenValue + "sanpham" + fileName + fileExtension;
                //Trỏ tới
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await HinhAnh.CopyToAsync(fileStream);
                }

                baiDang.HinhAnh = uniqueFileName;

                _context.Add(baiDang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDanhMuc"] = new SelectList(_context.DanhMucs, "Id", "Ten", baiDang.IdDanhMuc);
            return View(baiDang);
        }

        // GET: Admin/BaiDangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BaiDangs == null)
            {
                return NotFound();
            }

            var baiDang = await _context.BaiDangs.FindAsync(id);
            if (baiDang == null)
            {
                return NotFound();
            }
            ViewData["IdDanhMuc"] = new SelectList(_context.DanhMucs, "Id", "Ten", baiDang.IdDanhMuc);
            return View(baiDang);
        }

        // POST: Admin/BaiDangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten,HinhAnh,NoiDung,Active,IdDanhMuc")] BaiDang baiDang, IFormFile HinhAnh)
        {
            if (id != baiDang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Lấy tên ảnh gốc
                    var fileName = Path.GetFileNameWithoutExtension(HinhAnh.FileName);
                    //Lấy đuôi chấm
                    var fileExtension = Path.GetExtension(HinhAnh.FileName);
                    //Tạo tên
                    var tenValue = _context.Entry(baiDang).Property("Ten").CurrentValue.ToString();
                    var uniqueFileName = tenValue + "sanpham" + fileName + fileExtension;
                    //Trỏ tới
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await HinhAnh.CopyToAsync(fileStream);
                    }

                    baiDang.HinhAnh = uniqueFileName;

                    _context.Update(baiDang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaiDangExists(baiDang.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDanhMuc"] = new SelectList(_context.DanhMucs, "Id", "Ten", baiDang.IdDanhMuc);
            return View(baiDang);
        }

        // GET: Admin/BaiDangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BaiDangs == null)
            {
                return NotFound();
            }

            var baiDang = await _context.BaiDangs
                .Include(b => b.IdDanhMucNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baiDang == null)
            {
                return NotFound();
            }

            return View(baiDang);
        }

        // POST: Admin/BaiDangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BaiDangs == null)
            {
                return Problem("Entity set 'BaochiTestContext.BaiDangs'  is null.");
            }
            var baiDang = await _context.BaiDangs.FindAsync(id);
            if (baiDang != null)
            {
                _context.BaiDangs.Remove(baiDang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaiDangExists(int id)
        {
          return (_context.BaiDangs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
