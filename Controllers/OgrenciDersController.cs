using Microsoft.AspNetCore.Mvc;
using AkademikWebAPI.Data;
using System.Linq;
using System.Collections.Generic;

namespace AkademikWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OgrenciDersController : ControllerBase
    {
        private readonly AkademikDbContext _context;

        public OgrenciDersController(AkademikDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult TumNotlariGetir()
        {
            var atamalar = _context.OgrenciDersler.ToList();
            var ogrenciler = _context.Ogrenciler.ToList();
            var dersler = _context.Dersler.ToList();

            // "Bilinmiyor" hatasını önleyen, C# hafızasında Trim destekli güvenli eşleştirme
            var kayitlar = atamalar.Select(od => {
                var ogr = ogrenciler.FirstOrDefault(o => o.OgrenciNo?.Trim() == od.OgrenciNo?.Trim());
                var drs = dersler.FirstOrDefault(d => d.Kod?.Trim() == od.DersKodu?.Trim());
                
                return new {
                    ogrenciNo = od.OgrenciNo,
                    ogrenciAdi = ogr != null ? ogr.Isim : "Bilinmiyor",
                    dersKodu = od.DersKodu,
                    dersAdi = drs != null ? drs.Isim : "Bilinmiyor",
                    parametreler = drs != null ? drs.Parametreler : new Dictionary<string, int>(),
                    notlar = od.Notlar
                };
            }).ToList();

            return Ok(kayitlar);
        }

        public class DersAtamaModel
        {
            public string? OgrenciNo { get; set; }
            public string? DersKodu { get; set; }
        }

        [HttpPost]
        public IActionResult DersAta([FromBody] DersAtamaModel atama)
        {
            if (!_context.Ogrenciler.Any(o => o.OgrenciNo == atama.OgrenciNo)) return NotFound(new { mesaj = "Öğrenci bulunamadı!" });
            if (!_context.Dersler.Any(d => d.Kod == atama.DersKodu)) return NotFound(new { mesaj = "Ders bulunamadı!" });
            if (_context.OgrenciDersler.Any(od => od.OgrenciNo == atama.OgrenciNo && od.DersKodu == atama.DersKodu)) 
                return BadRequest(new { mesaj = "Öğrenci bu dersi zaten alıyor!" });

            _context.OgrenciDersler.Add(new Models.OgrenciDers { OgrenciNo = atama.OgrenciNo, DersKodu = atama.DersKodu });
            _context.SaveChanges();
            return Ok(new { mesaj = "Ders öğrenciye başarıyla atandı!" });
        }

        public class NotGirisModel
        {
            public string? OgrenciNo { get; set; }
            public string? DersKodu { get; set; }
            public Dictionary<string, int>? GirilenNotlar { get; set; } // Dinamik JSON veri transferi
        }

        [HttpPut("not-giris")]
        public IActionResult NotGir([FromBody] NotGirisModel model)
        {
            var kayit = _context.OgrenciDersler.ToList().FirstOrDefault(od => od.OgrenciNo?.Trim() == model.OgrenciNo?.Trim() && od.DersKodu?.Trim() == model.DersKodu?.Trim());
            if (kayit == null) return NotFound(new { mesaj = "Öğrencinin böyle bir ders kaydı yok!" });

            kayit.Notlar = model.GirilenNotlar ?? new Dictionary<string, int>();
            _context.SaveChanges();
            return Ok(new { mesaj = "Dinamik notlar başarıyla işlendi!" });
        }
    }
}