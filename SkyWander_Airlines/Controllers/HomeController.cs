using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SkyWander_Airlines.Models;
using System.Diagnostics;


namespace SkyWander_Airlines.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UygulamaDbContext _context;

        public HomeController(UygulamaDbContext context)
        {
            _context = context;

        }

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private static Dictionary<string, string> users = new Dictionary<string, string>
        {
              { "g211210008@gmail.com", "sau" } 
         };


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AnaSayfa()
        {
            return View();
        }

        public IActionResult GirisYap()
        {
            ViewBag.Layout = "~/Views/Shared/_AdminLayout.cshtml";
            return View();

        }
  

        [HttpPost]
        public async Task<IActionResult> GirisYap(string Email, string Sifre)
        {
            // Admin kontrolü
            if (Email == "g211210008@gmail.com" && Sifre == "sau")
            {
                HttpContext.Session.SetString("User", Email);
                HttpContext.Session.SetString("Role", "Admin");
                return RedirectToAction("Index", "Admin");
            }

            // Diğer kullanıcılar için veritabanı sorgusu
            var kullanici = await _context.Uyeler
                                            .FirstOrDefaultAsync(u => u.Email == Email && u.Sifre == Sifre);

            if (kullanici != null)
            {
                HttpContext.Session.SetString("User", Email);
                HttpContext.Session.SetString("Role", "User");

                // İptal edilen biletlerin kontrolü
                var iptalEdilenBiletler = await _context.BiletBilgisi
                                                     .Include(b => b.Rota)
                                                     .Where(b => b.Email == Email)
                                                     .ToListAsync();


                if (iptalEdilenBiletler.Any())
                {
                    TempData["IptalBildirim"] = "Aşağıdaki seferleriniz iptal edilmiştir: " +
                                                 String.Join(", ", iptalEdilenBiletler.Select(b => $"{b.Nereden}-{b.Nereye}"));
                }

                return RedirectToAction("Index", "Hizmetler");
            }

            ViewBag.Error = "Geçersiz giriş bilgileri";
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("GirisYap","Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}