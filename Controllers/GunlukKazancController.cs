using Microsoft.AspNetCore.Mvc;
using KuaforWebSitesi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KuaforWebSitesi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GunlukKazancController : ControllerBase
    {


        private readonly KuaforDBContext db;

        public GunlukKazancController(KuaforDBContext context)
        {
            db = context;
        }


        [HttpGet]
        public IActionResult GunlukKazanc()
        {
            var calisanlar = db.Calisanlar.ToList();  

            var gunlukKazancListesi = calisanlar.Select(calisan =>
            {
                // Çalışanın yaptığı hizmetlerin toplam kazancını hesaplayalım
                var toplamKazanc = db.CalisanHizmetler
                    .Where(ch => ch.CalisanID == calisan.CalisanID)  
                    .Join(db.Hizmetler, ch => ch.HizmetID, h => h.HizmetID, (ch, h) => h.Fiyat) 
                    .Sum(fiyat => fiyat); 

              
                var gunlukKazanc = toplamKazanc / 28;

                return new
                {
                    CalisanID = calisan.CalisanID,
                    CalisanAd = calisan.CalisanAd,
                    CalisanSoyad = calisan.CalisanSoyad,
                    ToplamKazanc = toplamKazanc,
                    GunlukKazanc = gunlukKazanc
                };
            }).ToList();

            return Ok(gunlukKazancListesi); 
        }



    }
}

