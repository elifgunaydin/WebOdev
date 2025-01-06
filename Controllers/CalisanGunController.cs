using KuaforWebSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KuaforWebSitesi.Controllers
{
    public class CalisanGunController : Controller
    {


        private readonly KuaforDBContext db;

        public CalisanGunController(KuaforDBContext context)
        {
            db = context;
        }

        // Çalışanların çalışma saatlerini güncelleme
      
        public IActionResult UpdateCalisanGunlerHours()
        {
            // Tüm CalisanGunler kayıtlarını alıyoruz
            var calisanGunler = db.CalisanGunler.ToList();

            // Çalışanların çalışma saatlerini güncelliyoruz
            foreach (var calisanGun in calisanGunler)
            {
                calisanGun.BaslangicSaati = new TimeSpan(9, 0, 0);  // 09:00
                calisanGun.BitisSaati = new TimeSpan(18, 0, 0);    // 18:00
            }

            // Veritabanına kaydediyoruz
            db.SaveChanges();

         
            TempData["msj"] = "Çalışan saatleri başarıyla güncellenmiştir.";
            return RedirectToAction("Index");  
        }


        // Yeni çalışanlar için varsayılan çalışma saatleri ekleme
        public IActionResult AddDefaultWorkingHoursForNewEmployees()
        {

            var yeniCalisanGunler = db.Calisanlar
                .Select(c => new CalisanGun
                {
                    CalisanID = c.CalisanID,
                    GunID = 1,
                    BaslangicSaati = new TimeSpan(9, 0, 0),  // 09:00
                    BitisSaati = new TimeSpan(18, 0, 0)     // 18:00
                })
                .ToList();


            db.CalisanGunler.AddRange(yeniCalisanGunler);
            db.SaveChanges();


            TempData["msj"] = "Yeni çalışanlar için çalışma saatleri başarıyla eklendi.";
            return RedirectToAction("Index");
        }  

        // Çalışan saatlerini görüntüleme
        public IActionResult ViewCalisanGunler()
        {
            var calisanGunler = db.CalisanGunler
                .Include(cg => cg.Calisanlar)
                .Include(cg => cg.Gunler)
                .ToList();

            return View(calisanGunler); 
        }
    }
}

