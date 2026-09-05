using Microsoft.AspNetCore.Mvc;
using AkademikWebAPI.Data;
using AkademikWebAPI.Models;
using System.Linq;

namespace AkademikWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AkademisyenController : ControllerBase
    {
        private readonly AkademikDbContext _context;

        public AkademisyenController(AkademikDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AkademisyenleriGetir()
        {
            return Ok(_context.Akademisyenler.ToList());
        }

        [HttpPost]
        public IActionResult AkademisyenEkle([FromBody] Akademisyen hoca)
        {
            _context.Akademisyenler.Add(hoca);
            _context.SaveChanges();
            return Ok(new { mesaj = "Akademisyen başarıyla eklendi!" });
        }

        [HttpDelete("{id}")]
        public IActionResult AkademisyenSil(int id)
        {
            var silinecekHoca = _context.Akademisyenler.FirstOrDefault(a => a.AkademisyenId == id);
            if (silinecekHoca == null)
                return NotFound(new { mesaj = "Hata: Silinecek akademisyen bulunamadı!" });

            var hocaninDersleri = _context.Dersler.Where(d => d.AkademisyenId == id).ToList();
            foreach (var ders in hocaninDersleri)
            {
                ders.AkademisyenId = null;
            }

            _context.Akademisyenler.Remove(silinecekHoca);
            _context.SaveChanges();

            return Ok(new { mesaj = "Akademisyen başarıyla sistemden silindi!" });
        }
    }
}