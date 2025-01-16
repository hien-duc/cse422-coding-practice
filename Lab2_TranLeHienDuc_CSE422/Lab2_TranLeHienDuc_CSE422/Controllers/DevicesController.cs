using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab2_TranLeHienDuc_CSE422.Models;
using Lab2_TranLeHienDuc_CSE422.Data;

namespace Lab2_TranLeHienDuc_CSE422.Controllers
{
    public class DevicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DevicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        private void PopulateViewBags()
        {
            ViewBag.Categories = new SelectList(_context.DeviceCategories.OrderBy(c => c.Name), "Id", "Name");
            ViewBag.Users = new SelectList(_context.Users.OrderBy(u => u.FullName), "Id", "FullName");
            ViewBag.Statuses = new SelectList(
                Enum.GetValues(typeof(DeviceStatus))
                    .Cast<DeviceStatus>()
                    .Select(s => new { Id = (int)s, Name = s.ToString() }),
                "Id",
                "Name"
            );
        }

        public async Task<IActionResult> Index(int? categoryId, DeviceStatus? status, string searchString, int? userId)
        {
            var devices = _context.Devices
                .Include(d => d.DeviceCategory)
                .Include(d => d.User)
                .AsQueryable();

            if (categoryId.HasValue)
            {
                devices = devices.Where(d => d.DeviceCategoryId == categoryId);
            }

            if (status.HasValue)
            {
                devices = devices.Where(d => d.Status == status);
            }

            if (userId.HasValue)
            {
                devices = devices.Where(d => d.UserId == userId);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                devices = devices.Where(d => d.Name.Contains(searchString) || d.Code.Contains(searchString));
            }

            PopulateViewBags();
            return View(await devices.ToListAsync());
        }

        public IActionResult Create()
        {
            PopulateViewBags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Device device)
        {
            // Debug information
            var isModelValid = ModelState.IsValid;
            var modelStateErrors = string.Join("; ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));

            if (!isModelValid)
            {
                // Log or debug model state errors
                System.Diagnostics.Debug.WriteLine($"Model state errors: {modelStateErrors}");
                PopulateViewBags();
                return View(device);
            }

            try
            {
                // Check for duplicate device code
                var existingDevice = await _context.Devices.FirstOrDefaultAsync(d => d.Code == device.Code);
                if (existingDevice != null)
                {
                    ModelState.AddModelError("Code", "This device code already exists.");
                    PopulateViewBags();
                    return View(device);
                }

                // Debug information
                System.Diagnostics.Debug.WriteLine($"Creating device: Name={device.Name}, Code={device.Code}, CategoryId={device.DeviceCategoryId}");

                device.EntryDate = DateTime.Now;
                _context.Devices.Add(device);
                await _context.SaveChangesAsync();

                // Debug information
                System.Diagnostics.Debug.WriteLine("Device saved successfully");

                TempData["SuccessMessage"] = "Device created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the actual exception
                System.Diagnostics.Debug.WriteLine($"Error creating device: {ex.Message}");
                ModelState.AddModelError("", "An error occurred while saving the device. Please try again.");
            }

            PopulateViewBags();
            return View(device);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            PopulateViewBags();
            return View(device);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Device device)
        {
            if (id != device.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(device);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(device.Id))
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

            PopulateViewBags();
            return View(device);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Include(d => d.DeviceCategory)
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device != null)
            {
                _context.Devices.Remove(device);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.Id == id);
        }
    }
}