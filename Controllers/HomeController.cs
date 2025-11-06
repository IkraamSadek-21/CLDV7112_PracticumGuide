using System.Diagnostics;
using CLDV7112_PracticumGuide.Models;
using Microsoft.AspNetCore.Mvc;
using CLDV7112_PracticumGuide.Data;

namespace CLDV7112_PracticumGuide.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context) => _context = context;

        public IActionResult Index()
        {
            var data = _context.SensorReadings.OrderByDescending(r => r.RecordedAt).Take(10).ToList();
            return View(data);
        }
    }
}
