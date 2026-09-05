using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AkademikWebAPI.Data;
using AkademikWebAPI.Models;
using System.Linq;

namespace AkademikWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DersController : ControllerBase
    {
        private readonly AkademikDbContext _context;

        public DersController(AkademikDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult DersleriGetir()
        {
            var dersler = _context.Dersler
                .Include(d => d.Akademisyen)
                .Select(d => new
                {
                    kod = d.Kod, 
                    isim = d.Isim,
                    akademisyenId = d.AkademisyenId,
                    akademisyen = d.Akademisyen != null ? new 
                    { 
                        unvan = d.Akademisyen.Unvan, 
                        adSoyad = d.Akademisyen.AdSoyad 
                    } : null
                })
                .ToList();
            
            return Ok(dersler);
        }

        public class YeniDersModel
        {
            public string? Kod { get; set; }
            public string? Isim { get; set; }
            public Dictionary<string, int>? Parametreler { get; set; }
        }

        [HttpPost]
        public IActionResult YeniDersEkle([FromBody] YeniDersModel yeniDers)
        {
            if (_context.Dersler.Any(d => d.Kod == yeniDers.Kod))
                return BadRequest(new { mesaj = "Bu ders kodu zaten mevcut!" });

            var ders = new Ders { Kod = yeniDers.Kod, Isim = yeniDers.Isim };
            if (yeniDers.Parametreler != null && yeniDers.Parametreler.Count > 0)
            {
                ders.Parametreler = yeniDers.Parametreler;
            }

            _context.Dersler.Add(ders);
            _context.SaveChanges();
            return Ok(new { mesaj = "Ders başarıyla eklendi!" });
        }

        public class HocaGuncellemeModel
        {
            public int? AkademisyenId { get; set; }
        }

        [HttpPut("{kod}/hoca-ata")]
        public IActionResult HocaAta(string kod, [FromBody] HocaGuncellemeModel model)
        {
            var ders = _context.Dersler.FirstOrDefault(d => d.Kod == kod);
            if (ders == null) return NotFound(new { mesaj = "Hata: Ders bulunamadı!" });

            ders.AkademisyenId = model.AkademisyenId;
            _context.SaveChanges();
            return Ok(new { mesaj = "Dersin hocası güncellendi!" });
        }

        [HttpDelete("{kod}")]
        public IActionResult DersSil(string kod)
        {
            var silinecekDers = _context.Dersler.FirstOrDefault(d => d.Kod == kod);
            if (silinecekDers == null) return NotFound(new { mesaj = "Hata: Silinecek ders bulunamadı!" });

            _context.Dersler.Remove(silinecekDers);
            _context.SaveChanges();
            return Ok(new { mesaj = "Ders başarıyla silindi!" });
        }
    }
}