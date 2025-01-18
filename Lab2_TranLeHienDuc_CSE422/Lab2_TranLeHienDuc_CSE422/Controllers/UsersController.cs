using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab2_TranLeHienDuc_CSE422.Data;
using Lab2_TranLeHienDuc_CSE422.Models;

namespace Lab2_TranLeHienDuc_CSE422.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? roleId, string searchString)
        {
            var users = _context.Users
                .Include(u => u.Role)
                .Include(u => u.Devices)
                .AsQueryable();

            if (roleId.HasValue)
            {
                users = users.Where(u => u.RoleId == roleId);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => 
                    u.FullName.Contains(searchString) || 
                    u.Email.Contains(searchString) || 
                    u.PhoneNumber.Contains(searchString));
            }

            ViewBag.Roles = new SelectList(_context.Roles.OrderBy(r => r.Name), "Id", "Name");
            return View(await users.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Devices)
                    .ThenInclude(d => d.DeviceCategory)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Create()
        {
            ViewBag.Roles = _context.Roles.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Email,PhoneNumber,Password,RoleId")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Roles = _context.Roles.ToList();
            return View(user);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.Roles = _context.Roles.ToList();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,PhoneNumber,Password,RoleId")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            ViewBag.Roles = _context.Roles.ToList();
            return View(user);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try 
            {
                var user = await _context.Users
                    .Include(u => u.Devices)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    TempData["MessageType"] = "error";
                    TempData["Message"] = "User not found.";
                    return RedirectToAction(nameof(Index));
                }

                // Check if user has any devices
                if (user.Devices != null && user.Devices.Any())
                {
                    TempData["MessageType"] = "error";
                    TempData["Message"] = "Cannot delete user with assigned devices. Please reassign or remove devices first.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error deleting user: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
