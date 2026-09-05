using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AkademikWebAPI.Models 
{
    public class Ogrenci 
    {
        [Key]
        public string OgrenciNo { get; set; }
        public string Isim { get; set; }
        [NotMapped]
        public Dictionary<string, DersKaydi> Notlar { get; set; }
        
        public Ogrenci(string ogrenciNo, string isim)
        {
            OgrenciNo = ogrenciNo;
            Isim = isim;
            Notlar = new Dictionary<string, DersKaydi>();
        }
    }
}