using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AkademikWebAPI.Models
{
    public class Akademisyen
    {
        [Key]
        public int AkademisyenId { get; set; }

        public string AdSoyad { get; set; }
        public string Unvan { get; set; }
        public ICollection<Ders>? VerdigiDersler { get; set; }
    }
}
