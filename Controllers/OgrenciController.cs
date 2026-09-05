using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AkademikWebAPI.Data;
using AkademikWebAPI.Models;
using System.Linq;

namespace AkademikWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OgrenciController : ControllerBase
    {
        private readonly AkademikDbContext _context;

        public OgrenciController(AkademikDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetOgrenciler()
        {
            var tumAtamalar = _context.OgrenciDersler.Include(od => od.Ders).ToList();

            var ogrenciler = _context.Ogrenciler.ToList().Select(o => new
            {
                ogrenciNo = o.OgrenciNo,
                isim = o.Isim,
                dersler = tumAtamalar
                    .Where(od => od.OgrenciNo?.Trim() == o.OgrenciNo?.Trim())
                    .Select(od => od.Ders != null 
                        ? $"{od.Ders.Kod} - {od.Ders.Isim}" 
                        : od.DersKodu) 
                    .ToList()
            }).ToList();

            return Ok(ogrenciler); 
        }

        [HttpPost]
        public IActionResult YeniOgrenciEkle([FromBody] Ogrenci yeniOgrenci)
        {
            if (_context.Ogrenciler.Any(o => o.OgrenciNo == yeniOgrenci.OgrenciNo))
            {
                return BadRequest(new { mesaj = "Bu öğrenci numarası zaten kayıtlı!" });
            }
            _context.Ogrenciler.Add(yeniOgrenci);
            _context.SaveChanges();
            return Ok(new { mesaj = "Öğrenci başarıyla eklendi!" }); 
        }

        [HttpPut("{id}")]
        public IActionResult OgrenciGuncelle(string id, [FromBody] Ogrenci guncelOgrenci)
        {
            var ogrenci = _context.Ogrenciler.FirstOrDefault(o => o.OgrenciNo == id);
            if (ogrenci == null)
            {
                return NotFound(new { mesaj = "Öğrenci bulunamadı!" });
            }

            if (!string.IsNullOrEmpty(guncelOgrenci.Isim))
            {
                ogrenci.Isim = guncelOgrenci.Isim;
            }
            
            _context.SaveChanges();
            return Ok(new { mesaj = "Öğrenci başarıyla güncellendi!" }); 
        }

        [HttpDelete("{id}")]
        public IActionResult OgrenciSil(string id)
        {
            var silinecek = _context.Ogrenciler.FirstOrDefault(o => o.OgrenciNo == id);
            if (silinecek == null)
            {
                return NotFound(new { mesaj = "Silinecek öğrenci bulunamadı!" });
            }

            // GÜVENLİK KİLİDİ: 500 Hatası almamak için öğrencinin atanmış derslerini sistemden siliyoruz
            var aldigiDersler = _context.OgrenciDersler.Where(od => od.OgrenciNo == id).ToList();
            if (aldigiDersler.Any())
            {
                _context.OgrenciDersler.RemoveRange(aldigiDersler);
            }

            // Artık öğrenciyi güvenle uçurabiliriz
            _context.Ogrenciler.Remove(silinecek);
            _context.SaveChanges();
            return Ok(new { mesaj = "Öğrenci sistemden silindi!" }); 
        }
    }
}