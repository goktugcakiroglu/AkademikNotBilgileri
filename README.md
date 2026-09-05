# 🎓 Akademik Yönetim Sistemi (AkademikWebAPI)

Bu proje, üniversite veya eğitim kurumları için geliştirilmiş, **ASP.NET Core Web API** ve **Vanilla JavaScript** kullanılarak inşa edilmiş uçtan uca bir öğrenci, akademisyen ve not yönetim sistemidir. Sistem, klasik "Vize/Final" dayatmasını ortadan kaldırarak her derse özel **dinamik JSON tabanlı değerlendirme parametreleri** (Lab, Proje, Ödev, Devamlılık vb.) sunar.

## ✨ Öne Çıkan Özellikler

* **Dinamik Parametrik Not Sistemi:** Her dersin değerlendirme kriteri (Örn: %20 Lab, %30 Vize, %50 Final) sisteme veritabanı üzerinden JSON formatında esnek olarak tanımlanabilir.
* **Anlık Ortalama Hesaplama:** Ön yüzde girilen notlar, dersin kendi ağırlık yüzdelerine göre JavaScript ile anında hesaplanarak dinamik olarak tabloya yansıtılır. Eksik not girişlerinde sistem otomatik uyarı verir.
* **İlişkisel Veritabanı:** Entity Framework Core Code-First yaklaşımı kullanılarak SQLite üzerinde `One-to-Many` (Akademisyen-Ders) ve `Many-to-Many` (Öğrenci-Ders) ilişkiler kurulmuştur.
* **SPA (Single Page Application) Deneyimi:** Herhangi bir frontend framework'ü kullanılmadan, yalnızca Vanilla JS ve Fetch API ile sayfa yenilenmeden çalışan, son derece hızlı ve duyarlı bir yönetim arayüzü inşa edilmiştir.
* **Güvenli Veri Akışı:** Yabancı anahtar (Foreign Key) kilitlenmelerini, JSON serileştirme döngülerini (Circular Reference) ve Null referans hatalarını önleyen sağlamlaştırılmış Backend mimarisi.

## 🛠️ Kullanılan Teknolojiler

* **Backend:** C#, ASP.NET Core Web API, Entity Framework Core 
* **Veritabanı:** SQLite
* **Frontend:** HTML5, CSS3, Vanilla JavaScript, Fetch API
* **Geliştirme & Versiyonlama:** .NET SDK, Git, GitHub

## 📂 Proje Dizin Yapısı

```text
AkademikWebAPI/
├── Controllers/
│   ├── AkademisyenController.cs     # Hoca CRUD işlemleri
│   ├── DersController.cs            # Dinamik ders oluşturma
│   ├── OgrenciController.cs         # Öğrenci CRUD işlemleri
│   └── OgrenciDersController.cs     # Ders atama ve dinamik not girişleri
├── Data/
│   └── AkademikDbContext.cs         # EF Core Veritabanı Bağlamı
├── Models/
│   ├── Akademisyen.cs
│   ├── Ders.cs                      # JSON parametre altyapılı
│   ├── Ogrenci.cs
│   └── OgrenciDers.cs               # JSON not altyapılı çoka-çok köprü
├── wwwroot/                         # SPA Frontend Klasörü
│   ├── index.html                   # Öğrenci Kayıt ve Listeleme
│   ├── dersler.html                 # Akademisyen ve Ders Parametre Yönetimi
│   ├── ders_secimi.html             # Öğrenciye Ders Atama Paneli
│   └── notlar.html                  # Dinamik Sınav Notları ve Ortalama Paneli
├── Program.cs                       # Uygulama Başlangıç Konfigürasyonları
└── appsettings.json
```

## 🚀 Kurulum ve Çalıştırma

Projeyi yerel bilgisayarınızda derleyip çalıştırmak için aşağıdaki adımları sırasıyla izleyin:

**1. Depoyu Klonlayın:**
```bash
git clone [https://github.com/goktugcakiroglu/AkademikNotBilgileri.git](https://github.com/goktugcakiroglu/AkademikNotBilgileri.git)
cd AkademikNotBilgileri
```

**2. Entity Framework Core Aracını Yükleyin (Daha önce yüklemediyseniz):**
```bash
dotnet tool install --global dotnet-ef
```

**3. Veritabanını İnşa Edin:**
```bash
dotnet ef migrations add DinamikSistemKurulumu
dotnet ef database update
```

**4. Projeyi Başlatın:**
```bash
dotnet run
```

Konsolda uygulamanın başladığına dair mesajı gördükten sonra tarayıcınızı açın ve uygulamanın `wwwroot` içindeki statik arayüzüne erişmek için `http://localhost:5220/index.html` (port numarası konsoldaki çıktıya göre değişebilir) adresine gidin.
