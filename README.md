# 🐾 Pati Dostu Veteriner Klinik Otomasyonu

Bu proje, günümüzde hızla artan evcil hayvan sahipliği oranına bağlı olarak veteriner kliniklerinde oluşan karmaşık iş yükünü yönetmek amacıyla geliştirilmiştir. 
 Manuel kayıt tutma süreçlerini dijitalleştirerek, hata payını minimize etmeyi ve klinik verimliliğini artırmayı hedefler.

## 🎯 Projenin Amacı
* Kliniklerdeki karmaşık veri trafiğini tek bir merkezden yönetilebilir hale getirmek.
* Hastaların tüm medikal geçmişine saniyeler içinde ulaşılmasını sağlamak.
* Randevu çakışmalarını ortadan kaldırmak ve kliniğin finansal/stok durumunu şeffaf bir şekilde izlemek.
* Veteriner hekimlerin idari işlerle vakit kaybetmesini önleyerek asıl odak noktaları olan hayvan sağlığına vakit ayırmalarına destek olmak.

## 🛠️ Kullanılan Teknolojiler
* **Yazılım Dili:** C# (Form Apps)
* **Veritabanı:** Microsoft SQL Server (MS SSMS)
* **Geliştirme Ortamı:** Visual Studio 2022

## 📋 Temel Modüller
* **Hasta ve Sahibi İşlemleri:** Evcil hayvan (tür, cins, yaş) ve sahibi bilgilerini ekleme, silme, güncelleme ve arama.
* **Randevu Yönetimi:** Klinik takvimi üzerinden randevu oluşturma ve geçmiş randevuların listelenmesi.
* **Tıbbi Geçmiş ve Aşı Takibi:** Hayvanlara yapılan aşıların, operasyonların ve kullanılan ilaçların tarihsel takibi.
* **Stok ve Ödeme İşlemleri:** İlaç/mama stok takibi ve yapılan işlemlerin faturalandırılması.

## ⚙️ Algoritma Akışı
1. **Giriş:** Kullanıcı adı ve şifre doğrulaması yapılır.
2. **Seçim:** Ana menüden ilgili işlem modülü (Hasta Kayıt, Randevu, Stok vb.) seçilir.
3. **Veri Girişi:** Seçilen modülde işlemler gerçekleştirilir ve veriler SQL veritabanına kaydedilir.
4. **Onay:** İşlem başarılı mesajı gösterilir ve ana menüye dönülür.

## 👥 Hedef Kullanıcılar
* Veteriner Hekimler 
* Klinik Asistanları 
* Klinik Yöneticileri

## 📸 Ekran Görüntüleri

Projenin temel arayüz tasarımları ve kullanım akışı aşağıda sunulmuştur:

### 🔐 Giriş Paneli
Veteriner ve yöneticilerin sisteme güvenli erişim sağladığı modül.
![Giriş Ekranı](image_646329.png)
### 🏠 Ana Menü (Dashboard)
Tüm klinik işlemlerinin merkez üssü.
![Ana Sayfa](https://github.com/user-attachments/assets/0139bfd8-1167-4741-b7a7-e577e9735526)

### 🐾 Hasta Kayıt ve Sorgulama
Evcil hayvanların ve sahiplerinin detaylı bilgilerinin yönetildiği alan.
![Hasta Kayıt Ekranı](https://github.com/user-attachments/assets/7c657621-110d-42ff-bb6f-5b94b3f397db)

### 📅 Randevu Yönetimi
Klinik takviminin ve randevu çakışmalarının kontrol edildiği modül.
![Randevu Sistemi](https://github.com/user-attachments/assets/820e9c20-21ed-4a0f-8054-2ad3d82c1632)

### 💊 Stok ve İlaç Takibi
İlaç envanterinin ve kritik stok seviyelerinin izlendiği ekran.
![Stok Takibi](https://github.com/user-attachments/assets/a3769b8e-1cec-4660-b6cc-108028d1ff61)
