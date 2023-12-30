using Microsoft.AspNetCore.Mvc;
using SkyWander_Airlines.Models;


namespace SkyWander_Airlines.Controllers
{
    public class YoneticiController : Controller
    {
        private readonly UygulamaDbContext _context;

        public YoneticiController(UygulamaDbContext context)
        {
            _context = context;

        }

        [HttpGet]
        public IActionResult Yonetici()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Yonetici(Rota model)
        {

            _context.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("AnaSayfa", "Home");

      
        }
    }
}
