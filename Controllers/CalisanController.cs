using KuaforWebSitesi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Linq;
using static System.Web.Razor.Parser.SyntaxConstants;
using System.Security.Cryptography;
using System.Text;

namespace KuaforWebSitesi.Controllers
{


    public class CalisanController : Controller
    {
       
        private readonly KuaforDBContext db;
        private readonly PasswordHasher<Calisan> _passwordHasher;
        private readonly ILogger<CalisanController> _logger;
       
        public CalisanController(KuaforDBContext context, PasswordHasher<Calisan> passwordHasher, ILogger<CalisanController> logger)
        {
            db = context;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        


        [HttpGet]
        public IActionResult CalisanGiris()
        {

            return View();
        }
        /*tamamlandı*/
        public IActionResult CalisanEkle()
        {
           
            ViewBag.Hizmetler = db.Hizmetler.ToList();
            ViewBag.Gunler = db.Gunler.ToList();       
            return View();
        }
        /*tamamlandı*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CalisanKaydet(Calisan calisan, int[] SecilenHizmetler, int[] SecilenGunler)
        {
            var varOlanEmailler = db.Calisanlar.FirstOrDefault(m => m.CalisanMail == calisan.CalisanMail);
            var varOlanTelefonlar = db.Calisanlar.FirstOrDefault(m => m.CalisanTelefon == calisan.CalisanTelefon);

            if (varOlanEmailler != null)
            {
                TempData["msj"] = "Bu e-posta adresi zaten kayıtlı.";
                return RedirectToAction("CalisanEkle"); // Hemen geri dön
            }
            if (varOlanTelefonlar != null)
            {
                TempData["msj"] = "Bu telefon numarası zaten kayıtlı.";
                return RedirectToAction("CalisanEkle"); // Hemen geri dön
            }

            if (!ModelState.IsValid)
            {
                TempData["msj"] = "Kayıt işlemi başarısız. Lütfen alanları kontrol edin.";
                return RedirectToAction("CalisanEkle");
            }


            try
            {
               
                calisan.CalisanSifre = _passwordHasher.HashPassword(calisan, calisan.CalisanSifre);

               
                db.Calisanlar.Add(calisan);
                await db.SaveChangesAsync();

                
                foreach (var hizmetID in SecilenHizmetler)
                {
                    db.CalisanHizmetler.Add(new CalisanHizmetler
                    {
                        CalisanID = calisan.CalisanID,
                        HizmetID = hizmetID
                    });
                }

                // Çalışanın seçtiği günleri ilişkilendiriyoruz
                foreach (var gunID in SecilenGunler)
                {
                    var baslangicSaati = new TimeSpan(9, 0, 0);  // 09:00
                    var bitisSaati = new TimeSpan(18, 0, 0);    // 18:00
                    db.CalisanGunler.Add(new CalisanGun
                    {
                        CalisanID = calisan.CalisanID,
                        GunID = gunID,
                        BaslangicSaati = baslangicSaati,
                        BitisSaati = bitisSaati
                    });
                }

                await db.SaveChangesAsync();

                // Kullanıcıya başarı mesajı veriyoruz
                TempData["msj"] = $"{calisan.CalisanAd} adlı çalışan kaydedildi.";
                return RedirectToAction("CalisanList");
            }
            catch (Exception ex)
            {
                TempData["msj"] = $"Kayıt işlemi sırasında bir hata oluştu: {ex.Message}";
                return RedirectToAction("CalisanEkle");
            }
        }




        /*tamamlandı*/
        [HttpGet]
        public IActionResult CalisanGuncelle(int id)
        {
            // id parametresinin doğru şekilde alındığını kontrol ediyoruz
            Console.WriteLine($"Gelen CalisanID: {id}");

            var calisan = db.Calisanlar.SingleOrDefault(m => m.CalisanID == id);
            if (calisan == null)
            {
                TempData["msj"] = $"Çalışan ID {id} veritabanında bulunamadı.";
                return RedirectToAction("CalisanList");
            }


            return View(calisan); // Mevcut çalışan bilgileriyle formu göster
        }


        /*tamamlandı*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CalisanGuncelle(Calisan calisan)
        {
            if (!ModelState.IsValid)
            {
                // Hatalı form durumunda mevcut modelle tekrar formu döndürüyoruz
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("Hata: " + error.ErrorMessage);  // Hata mesajlarını konsola yazdırıyoruz
                }

                TempData["msj"] = "Lütfen tüm alanları doldurun.";
                return View(calisan);
            }

            var mevcutCalisan = db.Calisanlar.FirstOrDefault(m => m.CalisanID == calisan.CalisanID);
            if (mevcutCalisan == null)
            {
                TempData["msj"] = "Çalışan bulunamadı.";
                return RedirectToAction("CalisanList");
            }

            // E-posta kontrolü, güncellenen çalışan hariç
            var varOlanEmailler = db.Calisanlar
                .FirstOrDefault(m => m.CalisanMail == calisan.CalisanMail && m.CalisanID != mevcutCalisan.CalisanID);
            if (varOlanEmailler != null)
            {
                TempData["msj"] = "Bu e-posta adresi başka bir çalışan tarafından kullanılıyor.";
                return View(calisan); // Hata mesajıyla birlikte tekrar formu döndürüyoruz
            }

            // Telefon kontrolü, güncellenen çalışan hariç
            var varOlanTelefonlar = db.Calisanlar
                .FirstOrDefault(m => m.CalisanTelefon == calisan.CalisanTelefon && m.CalisanID != mevcutCalisan.CalisanID);
            if (varOlanTelefonlar != null)
            {
                TempData["msj"] = "Bu telefon numarası başka bir çalışan tarafından kullanılıyor.";
                return View(calisan); // Hata mesajıyla birlikte tekrar formu döndürüyoruz
            }

            // Çalışan bilgilerini güncelle
            mevcutCalisan.CalisanAd = calisan.CalisanAd;
            mevcutCalisan.CalisanSoyad = calisan.CalisanSoyad;
            mevcutCalisan.CalisanMail = calisan.CalisanMail;
            mevcutCalisan.CalisanTelefon = calisan.CalisanTelefon;

            db.Calisanlar.Update(mevcutCalisan);
            await db.SaveChangesAsync();

            TempData["msj"] = "Çalışan başarıyla güncellendi.";
            return RedirectToAction("CalisanList"); // Güncellenmiş çalışan listesine yönlendir
        }

        public async Task<IActionResult> CalisanSil(int id)
        {
            // Çalışanı veritabanından buluyoruz
            var calisan = await db.Calisanlar
                                  .Include(c => c.CalisanHizmetler)   // Hizmetler ile ilişkili verileri yükle
                                  .Include(c => c.CalisanGunler)      // Günlerle ilişkili verileri yükle
                                  .FirstOrDefaultAsync(c => c.CalisanID == id);

            if (calisan == null)
            {
                TempData["msj"] = "Silinecek çalışan bulunamadı.";
                return RedirectToAction("CalisanList");
            }

            try
            {
                // Çalışanın randevularını silme işlemi
                db.Randevular.RemoveRange(db.Randevular.Where(r => r.CalisanID == calisan.CalisanID));

                // Çalışanın hizmetlerini ve çalışma günlerini siliyoruz
                db.CalisanHizmetler.RemoveRange(calisan.CalisanHizmetler);
                db.CalisanGunler.RemoveRange(calisan.CalisanGunler);

                // Çalışanı siliyoruz
                db.Calisanlar.Remove(calisan);

                // Değişiklikleri veritabanına kaydediyoruz
                await db.SaveChangesAsync();

                TempData["msj"] = $"{calisan.CalisanAd} adlı çalışan başarıyla silindi.";
            }
            catch (Exception ex)
            {
                TempData["msj"] = $"Silme işlemi sırasında bir hata oluştu: {ex.Message}";
            }

            return RedirectToAction("CalisanList");
        }

        public IActionResult CalisanGoster()
        {
            var calisanlar = db.Calisanlar
                                .Include(c => c.CalisanGunler)
                                .ThenInclude(cg => cg.Gunler)  // CalisanGun'ün Gun ile ilişkisini alıyoruz
                                .Include(c => c.CalisanHizmetler)
                                .ThenInclude(ch => ch.Hizmet)
                                .ThenInclude(h => h.HizmetKategoriler)  // Hizmet'in kategorilerini dahil ediyoruz
                                .ToList();

            ViewBag.Msj = TempData["msj"];
            return View(calisanlar);
        }




        public IActionResult CalisanList()
        {
            var calisanlar = db.Calisanlar.ToList();
            ViewBag.Msj = TempData["msj"];
            return View(calisanlar);
        }

    }
}
