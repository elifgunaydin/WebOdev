using KuaforWebSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using System.ComponentModel.DataAnnotations;
namespace KuaforWebSitesi.Controllers
{
    public class RandevuController : Controller
    {
        private readonly KuaforDBContext db;

        public RandevuController(KuaforDBContext context)
        {
            db = context;
        }
        [HttpGet]
        public ActionResult RandevuAl()
        {
            var musteriID = HttpContext.Session.GetString("MusteriID");
            if (string.IsNullOrEmpty(musteriID))
            {
                TempData["msj"] = "Lütfen giriş yapın.";
                return RedirectToAction("MusteriGiris", "Musteri");
            }
            var hizmetler = db.Hizmetler.ToList();
            if (hizmetler == null || hizmetler.Count == 0)
            {
                TempData["Error"] = "Hizmetler mevcut değil.";
            }
            else
            {
                ViewData["MusteriID"] = musteriID;
              
                ViewData["Hizmetler"] = hizmetler;
              
               
            }
            return View();
        }



      
        [HttpPost]

        public IActionResult RandevuAlPost(int hizmetID, int calisanID, string randevuGunu)
        {
           
            var musteriID = HttpContext.Session.GetString("MusteriID");
            if (string.IsNullOrEmpty(musteriID))
            {
                TempData["msj"] = "Lütfen giriş yapın.";
                return RedirectToAction("MusteriGiris"); 
            }
          
            if (hizmetID == 0 || calisanID == 0 || string.IsNullOrEmpty(randevuGunu))
            {
                TempData["msj"] = "Lütfen tüm alanları doldurduğunuzdan emin olun.";
                return RedirectToAction("RandevuAl");
            }

          
            DateTime selectedDate;
            if (!DateTime.TryParse(randevuGunu, out selectedDate))
            {
                TempData["msj"] = "Geçersiz tarih formatı.";
                return RedirectToAction("RandevuAl");
            }

            
            TempData["msj"] = "Randevunuz başarıyla alınmıştır.";
            return RedirectToAction("RandevuAl");
        }


        [HttpGet]
        public async Task<IActionResult> GetCalisanGunler(int calisanID)
        {
           
            var calisanGunler = await db.CalisanGunler
                .Where(cg => cg.CalisanID == calisanID)
                .Select(cg => cg.GunID)
                .ToListAsync();

            
            if (calisanGunler == null || calisanGunler.Count == 0)
            {
                return Json(new { error = "Hiç gün bulunamadı." });
            }

           
            var gunAdlari = await db.Gunler
                .Where(g => calisanGunler.Contains(g.GunID))  
                .Select(g => g.GunAdi)  
                .ToListAsync();
            Console.WriteLine(string.Join(", ", gunAdlari)); 
            return Json(gunAdlari); 
        }




        [HttpGet]
        public JsonResult GetCalisanlar(int hizmetID)
        {
            
            var calisanlar = db.Calisanlar
                .Where(c => c.CalisanHizmetler.Any(ch => ch.HizmetID == hizmetID))
                .Select(c => new
                {
                    calisanID = c.CalisanID,
                    ad = c.CalisanAd,
                    soyad = c.CalisanSoyad
                })
                .ToList();

            return Json(calisanlar);
        }
        public async Task<List<TimeSpan>> GetUygunSaatler(int calisanID, DateTime tarih, int hizmetID)
        {
            var hizmet = await db.Hizmetler
                .FirstOrDefaultAsync(h => h.HizmetID == hizmetID);

            if (hizmet == null)
            {
                throw new Exception("Seçilen hizmet bulunamadı.");
            }

            TimeSpan hizmetSuresi = hizmet.Sure;

           
            var gunAdi = tarih.DayOfWeek.ToString(); 
            var calisanGun = await db.CalisanGunler
                .FirstOrDefaultAsync(cg => cg.CalisanID == calisanID && cg.GunID == (int)tarih.DayOfWeek);

            if (calisanGun == null)
            {
                throw new Exception("Bu gün için çalışma saatleri bulunamadı.");
            }

            var mevcutRandevular = await db.Randevular
                .Where(r => r.CalisanID == calisanID && r.Tarih.Date == tarih.Date)
                .ToListAsync();

            var uygunSaatler = new List<TimeSpan>();

          
            for (var saat = calisanGun.BaslangicSaati;
                 saat < calisanGun.BitisSaati;
                 saat = saat.Add(hizmetSuresi)) 
            {
                bool cakismaVar = mevcutRandevular.Any(r =>
                {
                    var randevuBaslangic = r.Saat;
                    TimeSpan randevuBitis = randevuBaslangic.Add(hizmetSuresi); 
                    TimeSpan yeniRandevuBitis = saat.Add(hizmetSuresi); 

                    return (saat >= randevuBaslangic && saat < randevuBitis) ||
                           (yeniRandevuBitis > randevuBaslangic && yeniRandevuBitis <= randevuBitis);
                });

                if (!cakismaVar)
                {
                    uygunSaatler.Add(saat);
                }
            }

            return uygunSaatler;
        }





        [HttpPost]
      
        public IActionResult RandevuAlFinal(Randevu randevu)
        {
            // Validate required fields
            if (randevu.HizmetID == 0 || randevu.CalisanID == 0 || randevu.MusteriID == 0)
            {
                TempData["msj"] = "Lütfen tüm alanları doğru doldurduğunuzdan emin olun.";
                return RedirectToAction("RandevuAl");
            }

            try
            {
                // Saat ve tarih bilgilerini birleştirme
                if (randevu.Tarih < DateTime.Now.Date)
                {
                    TempData["msj"] = "Geçmiş bir tarih seçilemez.";
                    return RedirectToAction("RandevuAl"); // Bu işlemdeki yönlendirme kullanıcıyı tekrar doğru sayfaya yönlendirir.
                }

                // Veritabanına kaydetme
                db.Randevular.Add(randevu);
                db.SaveChanges();

                // Başarı mesajı
                TempData["msj"] = "Randevunuz başarıyla kaydedildi.";
                return RedirectToAction("ProfilGoruntule","Musteri" ,new { id = randevu.RandevuID });
            }
            catch (Exception ex)
            {
                TempData["msj"] = $"Hata oluştu: {ex.Message}";
                return RedirectToAction("RandevuAl");  // Hata durumunda yönlendirme tekrar form sayfasına yapılır.
            }
        }


        public IActionResult RandevuListesi()
        {
            var randevular = db.Randevular
               .Include(r => r.Hizmetler)
               .Include(r => r.Calisan).
               Include(r => r.Musteri)
               .ToList();
            return View(randevular);
        }

        // Randevuyu onaylama işlemi
        [HttpPost]
        public IActionResult RandevuOnayla(int randevuID)
        {
            var randevu = db.Randevular.Find(randevuID);
            if (randevu != null)
            {
                randevu.Durum = "Onaylandı";
                db.SaveChanges();
                TempData["msj"] = "Randevu başarıyla onaylandı.";
            }
            else
            {
                TempData["msj"] = "Randevu bulunamadı.";
            }
            return RedirectToAction("RandevuListesi");
        }

        // Randevu silme işlemi
        [HttpPost]
        public IActionResult RandevuSil(int randevuID)
        {
            var randevu = db.Randevular.Find(randevuID);
            if (randevu != null)
            {
                db.Randevular.Remove(randevu);
                db.SaveChanges();
                TempData["msj"] = "Randevu başarıyla silindi.";
            }
            else
            {
                TempData["msj"] = "Randevu bulunamadı.";
            }
            return RedirectToAction("RandevuListesi");
        }


     

    }
}