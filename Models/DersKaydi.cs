using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AkademikWebAPI.Models
{
    public class DersKaydi
    {
        [NotMapped]
        public Dictionary<string, double> Detaylar { get; set; } 
        
        public double Ortalama { get; set; }

        public DersKaydi()
        {
            Detaylar = new Dictionary<string, double>();
            Ortalama = 0.0;
        }
    }
}