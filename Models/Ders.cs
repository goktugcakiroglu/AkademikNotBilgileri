using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace AkademikWebAPI.Models
{
    public class Ders
    {
        [Key]
        public string Kod { get; set; }
        public string Isim { get; set; }
        public int? AkademisyenId { get; set; }
        
        [ForeignKey("AkademisyenId")]
        public Akademisyen? Akademisyen { get; set; }

        public string ParametrelerJson { get; set; } = "{}";

        [NotMapped]
        public Dictionary<string, int> Parametreler
        {
            get => string.IsNullOrEmpty(ParametrelerJson) ? new Dictionary<string, int>() : JsonSerializer.Deserialize<Dictionary<string, int>>(ParametrelerJson);
            set => ParametrelerJson = JsonSerializer.Serialize(value);
        }
    }
}