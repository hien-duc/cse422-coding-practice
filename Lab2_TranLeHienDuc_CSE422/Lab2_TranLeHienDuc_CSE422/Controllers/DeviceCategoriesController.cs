using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab2_TranLeHienDuc_CSE422.Models;
using Lab2_TranLeHienDuc_CSE422.Data;

namespace Lab2_TranLeHienDuc_CSE422.Controllers
{
    public class DeviceCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeviceCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var categories = _context.DeviceCategories
                .Include(c => c.Devices)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(c => 
                    c.Name.Contains(searchString) || 
                    c.Description.Contains(searchString));
            }

            return View(await categories.ToListAsync());
        }

        // GET: DeviceCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceCategory = await _context.DeviceCategories
                .Include(c => c.Devices)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (deviceCategory == null)
            {
                return NotFound();
            }

            return View(deviceCategory);
        }

        // GET: DeviceCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] DeviceCategory deviceCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deviceCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deviceCategory);
        }

        // GET: DeviceCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceCategory = await _context.DeviceCategories.FindAsync(id);
            if (deviceCategory == null)
            {
                return NotFound();
            }
            return View(deviceCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] DeviceCategory deviceCategory)
        {
            if (id != deviceCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deviceCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceCategoryExists(deviceCategory.Id))
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
            return View(deviceCategory);
        }

        // GET: DeviceCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceCategory = await _context.DeviceCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deviceCategory == null)
            {
                return NotFound();
            }

            return View(deviceCategory);
        }

        // POST: DeviceCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deviceCategory = await _context.DeviceCategories.FindAsync(id);
            if (deviceCategory != null)
            {
                _context.DeviceCategories.Remove(deviceCategory);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceCategoryExists(int id)
        {
            return _context.DeviceCategories.Any(e => e.Id == id);
        }
    }
}
