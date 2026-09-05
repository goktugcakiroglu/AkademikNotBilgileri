using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace AkademikWebAPI.Models
{
    public class OgrenciDers
    {
        public string OgrenciNo { get; set; }
        
        [ForeignKey("OgrenciNo")]
        public Ogrenci? Ogrenci { get; set; }

        public string DersKodu { get; set; }
        
        [ForeignKey("DersKodu")]
        public Ders? Ders { get; set; }

        public string NotlarJson { get; set; } = "{}";

        [NotMapped]
        public Dictionary<string, int> Notlar
        {
            get => string.IsNullOrEmpty(NotlarJson) ? new Dictionary<string, int>() : JsonSerializer.Deserialize<Dictionary<string, int>>(NotlarJson);
            set => NotlarJson = JsonSerializer.Serialize(value);
        }
    }
}