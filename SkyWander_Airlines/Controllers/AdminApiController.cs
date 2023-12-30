using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyWander_Airlines.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SkyWander_Airlines.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminApiController:ControllerBase
    {
        private readonly UygulamaDbContext _context;

        public AdminApiController(UygulamaDbContext context)
        {
            _context = context;
        }

    
        [HttpGet("Uyeler")]
        public async Task<ActionResult<IEnumerable<Uyeler>>> GetUyeler()
        {
            return await _context.Uyeler.ToListAsync();
        }


        [HttpGet("Rota")]
        public async Task<ActionResult<IEnumerable<Rota>>> GetRota()
        {
            return await _context.Rota.ToListAsync();
        }


        [HttpDelete("UyeSil/{id}")]
        public async Task<IActionResult> DeleteUye(int id)
        {
            var uye = await _context.Uyeler.FindAsync(id);
            if (uye == null)
            {
                return NotFound();
            }

            _context.Uyeler.Remove(uye);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("SeferiIptalEt/{id}")]
        public async Task<IActionResult> DeleteSefer(int id)
        {
            var sefer = await _context.Rota.FindAsync(id);
           
            if (sefer == null)
            {
                return NotFound();
            }

   
            var biletler = await _context.BiletBilgisi.Where(b => b.RotaId == id).ToListAsync();
            foreach (var bilet in biletler)
            {
                bilet.Durum = false;
                await _context.SaveChangesAsync();
            }
           
            _context.Rota.Remove(sefer);
            await _context.SaveChangesAsync();
            return NoContent();
        }


   

    }
}
