using System.Collections.Generic;
using AkademikWebAPI.Models; 

namespace AkademikWebAPI.Engine 
{
    public class HesaplamaMotoru
    {
        public static double YilSonuNotuHesapla(Ders dersObjesi, Dictionary<string, double> ogrenciNotlari)
        {
            double toplamNot = 0.0;

            foreach (var bilesen in dersObjesi.Parametreler)
            {
                string bilesenAdi = bilesen.Key;
                int yuzde = bilesen.Value; 

                ogrenciNotlari.TryGetValue(bilesenAdi, out double alinanNot);
                toplamNot += alinanNot * (yuzde / 100.0); 
            }

            return toplamNot;
        }
    }
}