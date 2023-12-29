using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyWander_Airlines.Models;
using System.Drawing;
using System.Security.Cryptography;

namespace SkyWander_Airlines.Controllers
{
    public class UyeController : Controller
    {
        private readonly UygulamaDbContext _context;

        public UyeController(UygulamaDbContext context)
        {
            _context = context;

        }
        [HttpGet]



        public IActionResult UyeOl()
        {
            return View();
        }

        private string GetUserRole()
        {
            return HttpContext.Session.GetString("Role") ?? "User";
        }




        [HttpPost]
        public async Task<IActionResult> UyeOl(Uyeler model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("AnaSayfa", "Home");
            }
            return View(model);
        }


        public IActionResult UyeBilgileriniGuncelle()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UyeBilgileriniGuncelle(string Email, string Sifre)
        {
            var kullanici = await _context.Uyeler
                .FirstOrDefaultAsync(x => x.Email == Email && x.Sifre == Sifre);

            if (kullanici != null)
            {
                return RedirectToAction("Profil", new { id = kullanici.ID });
            }

            ViewBag.ErrorMessage = "Geçersiz E-Mail veya Şifre";
            return View();
        }





        [HttpGet]
        public async Task<IActionResult> Profil(int id)
        {
            var kullanici = await _context.Uyeler.FindAsync(id);
            if (kullanici == null)
            {
                return NotFound();
            }
            return View(kullanici);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profil(Uyeler uyelerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(uyelerModel);
            }

            var kullaniciInDb = await _context.Uyeler.FindAsync(uyelerModel.ID);
            if (kullaniciInDb == null)
            {
                // Kullanıcı bulunamadıysa hata mesajı göster
                ViewBag.ErrorMessage = "Kullanıcı bulunamadı.";
                return View(uyelerModel);
            }
            kullaniciInDb.Ad = uyelerModel.Ad;
            kullaniciInDb.Soyad = uyelerModel.Soyad;
            kullaniciInDb.Telefon = uyelerModel.Telefon;
            kullaniciInDb.DogumTarihi = uyelerModel.DogumTarihi;
            kullaniciInDb.Ulke = uyelerModel.Ulke;
            kullaniciInDb.Sehir = uyelerModel.Sehir;
            kullaniciInDb.Adres = uyelerModel.Adres;
            kullaniciInDb.Email = uyelerModel.Email;
            kullaniciInDb.Sifre = uyelerModel.Sifre;


            await _context.SaveChangesAsync();
            return RedirectToAction("AnaSayfa", "Home");
        }




    }


}


