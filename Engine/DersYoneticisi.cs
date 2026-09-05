
using AkademikWebAPI.Models;

namespace AkademikWebAPI.Engine
{
    public static class DersYoneticisi
    {
        // UI sadece oluşturduğu Ders nesnesini buraya fırlatır
        /*public static bool YeniDersEkle(Ders yeniDers, out string mesaj)
        {
            if (VeriDeposu.Dersler.ContainsKey(yeniDers.Kod))
            {
                mesaj = $"\n[HATA] {yeniDers.Kod} kodlu ders zaten kayıtlı!";
                return false;
            }

            if (!yeniDers.SablonKontrolu())
            {
                mesaj = $"\n[HATA] Yüzde toplamı 100 olmalıdır! (Şu an: {yeniDers.Parametreler.Values.Sum()})";
                return false;
            }

            VeriDeposu.Dersler.Add(yeniDers.Kod, yeniDers);
            TxtDosyaYonetici.DersleriKaydet(VeriDeposu.Dersler);

            mesaj = $"\n[BAŞARILI] {yeniDers.Kod} sisteme eklendi!";
            return true;
        }

        public static bool OgrenciyiDerseKaydet(string ogrNo, string dersKodu, out string mesaj)
        {
            if (!VeriDeposu.Ogrenciler.ContainsKey(ogrNo) || !VeriDeposu.Dersler.ContainsKey(dersKodu))
            {
                mesaj = "[HATA] Öğrenci veya ders bulunamadı!";
                return false;
            }

            var ogrenci = VeriDeposu.Ogrenciler[ogrNo];
            if (ogrenci.Notlar.ContainsKey(dersKodu))
            {
                mesaj = "\n[BİLGİ] Öğrenci zaten bu derse kayıtlı!";
                return false; // İşlem yapılmadı
            }

            ogrenci.Notlar.Add(dersKodu, new DersKaydi());
            TxtDosyaYonetici.NotlariKaydet();
            mesaj = $"\n[BAŞARILI] {ogrenci.Isim}, {dersKodu} dersine kaydedildi!";
            return true;
        }

        public static bool DersSil(string dersKodu, out string mesaj)
        {
            if (VeriDeposu.Dersler.Remove(dersKodu))
            {
                // Dersi silince, onu alan tüm öğrencilerin karnesinden de sil (Cascade Delete)
                foreach (var ogr in VeriDeposu.Ogrenciler.Values)
                {
                    ogr.Notlar.Remove(dersKodu);
                }
                TxtDosyaYonetici.DersleriKaydet(VeriDeposu.Dersler);
                TxtDosyaYonetici.NotlariKaydet();

                mesaj = $"\n[BAŞARILI] {dersKodu} dersi ve tüm notları silindi!";
                return true;
            }

            mesaj = "\n[HATA] Sistemde böyle bir ders bulunamadı.";
            return false;
        }*/

    }
}