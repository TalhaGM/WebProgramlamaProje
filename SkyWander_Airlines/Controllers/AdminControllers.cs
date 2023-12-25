using Microsoft.AspNetCore.Mvc;
using SkyWander_Airlines.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace SkyWander_Airlines.Controllers
{
    public class AdminControllers:Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public AdminControllers(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return Unauthorized(); 
            }
            ViewBag.Layout = GetUserRole() == "Admin" ? "_AdminLayout" : "_UserLayout";
       
            return View();
        }

   

        private string GetUserRole()
        {
            return HttpContext.Session.GetString("Role") ?? "User";
        }

  
        public IActionResult UyeleriListele()
        {
            return View();
        }


        public async Task<IActionResult> UyeSil(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.DeleteAsync($"http://localhost:5279/api/AdminApi/UyeSil/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("UyeleriListele");
            }
            return View("Error"); 
        }

        public IActionResult SeferleriDuzenle()
        {
            return View();
        }

        public IActionResult CikisYap()
        {
            return RedirectToAction("AnaSayfa", "Home");
        }
    }
}
