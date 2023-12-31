using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyWander_Airlines.Models;

namespace SkyWander_Airlines.Controllers
{
    public class HizmetlerController : Controller
    {
        private readonly UygulamaDbContext _context;

        public HizmetlerController(UygulamaDbContext context)
        {
            _context = context;

        }

        public IActionResult Hizmetler()
        {

      
            return View();
        }


        private string GetUserRole()
        {
            return HttpContext.Session.GetString("Role") ?? "User";
        }

        public IActionResult Index()
        {
            if (TempData["IptalBildirim"] != null)
            {
                ViewBag.IptalBildirim = TempData["IptalBildirim"].ToString();
            }

            ViewBag.Layout = GetUserRole() == "Admin" ? "_AdminLayout" : "_KullaniciLayout";
            return View();
        }

        [HttpPost]
        public IActionResult Hizmetler(string Email, string Sifre)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Geçersiz kullanıcı adı veya şifre";
                return RedirectToAction("GirisYap", "Home");
            }

            var kullanici = _context.Kullanici.FirstOrDefault(x => x.Email == Email);

            if (kullanici != null && kullanici.Sifre == Sifre)
            {
                return View();
            }

            ViewBag.ErrorMessage = "Geçersiz kullanıcı adı veya şifre";
            return RedirectToAction("GirisYap", "Home");

        }

        public IActionResult Güvenlik(string Email, string Sifre)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Geçersiz kullanıcı adı veya şifre";
                return RedirectToAction("GirisYap", "Home");
            }

            var kullanici = _context.Kullanici.FirstOrDefault(x => x.Email == Email);

            if (kullanici != null && kullanici.Sifre == Sifre)
            {
                return View();
            }

            ViewBag.ErrorMessage = "Geçersiz kullanıcı adı veya şifre";
            return RedirectToAction("GirisYap", "Home");

        }






        [HttpPost]
        public IActionResult BiletAl(string Nereden, string Nereye, DateTime Tarih)
        {
            var guzergahlar = _context.Yonetici.ToList();

            TempData["Nereden"] = Nereden;
            TempData["Nereye"] = Nereye;
            TempData["Tarih"] = Tarih;

            return View(guzergahlar);
        }



        public IActionResult BiletIadeEt(string Nereden, string Nereye, DateTime Tarih)
        {
            var guzergahlar = _context.Yonetici.ToList();

            TempData["Nereden"] = Nereden;
            TempData["Nereye"] = Nereye;
            TempData["Tarih"] = Tarih;

            return View(guzergahlar);
        }

        [HttpGet]
        public async Task<IActionResult> BiletSatinAl(int id)
        {
            var rota = await _context.Rota
             .FirstOrDefaultAsync(r => r.ID == id);

            var secilenKoltuklar = await _context.BiletBilgisi
                                 .Where(b => b.RotaId == id)
                                 .Select(b => b.KoltukNumarasi)
                                 .ToListAsync();

            ViewBag.SecilenKoltuklar = secilenKoltuklar;

            if (rota != null)
            {
                return View(rota);
            }

            return View("RotaBulunamadi");
        }


        [HttpPost]
        public async Task<IActionResult> BiletSatinAl(BiletBilgisi bilet)
        {

            var rota = await _context.Rota.FindAsync(bilet.RotaId);

            if (rota == null)
            {
                return View("Error");
            }

            var biletBilgisi = new BiletBilgisi
            {

                Ad = bilet.Ad,
                Soyad = bilet.Soyad,
                Telefon = bilet.Telefon,
                DogumTarihi = bilet.DogumTarihi,
                Email = bilet.Email,
                KoltukNumarasi = bilet.KoltukNumarasi,
                Adres = bilet.Adres,
                RotaId = rota.ID,
                Nereden = rota.Nereden,
                Nereye = rota.Nereye,
                Tarih = rota.Tarih,
                VarisZamani = rota.VarisZamani,
                KalkisZamani = rota.KalkisZamani,
                UcakTipi = rota.UcakTipi,
                Durum = true,
          

            };

            _context.BiletBilgisi.Add(biletBilgisi);
            await _context.SaveChangesAsync();

            return RedirectToAction("BiletBilgisi", "Hizmetler", new { id = biletBilgisi.Id });

        }

        public async Task<IActionResult> BiletBilgisi(int id)
        {
            var bilet = await _context.BiletBilgisi.FindAsync(id);
            if (bilet == null)
            {

                return NotFound();
            }

            return View(bilet);
        }



        [HttpGet]
        [Route("api/bilet/{id}")]
        public async Task<IActionResult> GetBilet(int id)
        {
            var bilet = await _context.BiletBilgisi.FirstOrDefaultAsync(b => b.Id == id);
            if (bilet == null)
            {
                return NotFound();
            }

            return Ok(bilet);
        }

        public IActionResult CikisYap()
        {
            return RedirectToAction("AnaSayfa", "Home");
        }


    }
}
