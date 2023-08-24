using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using baochi_test.Models;
using PagedList.Core;
using baochi_test.Areas.Admin.Models;

namespace baochi_test.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DanhMucsController : Controller
    {
        private readonly BaochiTestContext _context;

        public DanhMucsController(BaochiTestContext context)
        {
            _context = context;
        }
        [Authentication]
        // GET: Admin/DanhMucs
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 4;
            var baochiTestContext = _context.DanhMucs.Include(b => b.BaiDangs);
            IPagedList<DanhMuc> models = new PagedList<DanhMuc>(baochiTestContext, pageNumber, pageSize);
            return View(models);
        }

        [Authentication]
        // GET: Admin/DanhMucs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DanhMucs == null)
            {
                return NotFound();
            }

            var danhMuc = await _context.DanhMucs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (danhMuc == null)
            {
                return NotFound();
            }

            return View(danhMuc);
        }

        [Authentication]
        // GET: Admin/DanhMucs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/DanhMucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,MoTa")] DanhMuc danhMuc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danhMuc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(danhMuc);
        }

        [Authentication]
        // GET: Admin/DanhMucs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DanhMucs == null)
            {
                return NotFound();
            }

            var danhMuc = await _context.DanhMucs.FindAsync(id);
            if (danhMuc == null)
            {
                return NotFound();
            }
            return View(danhMuc);
        }

        [Authentication]
        // POST: Admin/DanhMucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten,MoTa")] DanhMuc danhMuc)
        {
            if (id != danhMuc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhMuc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhMucExists(danhMuc.Id))
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
            return View(danhMuc);
        }

        [Authentication]
        // GET: Admin/DanhMucs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DanhMucs == null)
            {
                return NotFound();
            }

            var danhMuc = await _context.DanhMucs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (danhMuc == null)
            {
                return NotFound();
            }

            return View(danhMuc);
        }

        [Authentication]
        // POST: Admin/DanhMucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DanhMucs == null)
            {
                return Problem("Entity set 'BaochiTestContext.DanhMucs'  is null.");
            }
            var danhMuc = await _context.DanhMucs.FindAsync(id);
            if (danhMuc != null)
            {
                _context.DanhMucs.Remove(danhMuc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhMucExists(int id)
        {
          return (_context.DanhMucs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
