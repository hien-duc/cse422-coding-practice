using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lab2_TranLeHienDuc_CSE422.Models;
using Lab2_TranLeHienDuc_CSE422.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab2_TranLeHienDuc_CSE422.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var deviceCount = await _context.Devices.CountAsync();
            var categoryCount = await _context.DeviceCategories.CountAsync();
            var userCount = await _context.Users.CountAsync();

            ViewBag.DeviceCount = deviceCount;
            ViewBag.CategoryCount = categoryCount;
            ViewBag.UserCount = userCount;

            return View();
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
