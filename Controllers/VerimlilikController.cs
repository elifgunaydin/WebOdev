using KuaforWebSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Http;
//burada  rest apı kullanıldıııııı/////


/*https://localhost:7035/api/Verimlilik/*/
//https://localhost:7035/api/Verimlilik  postman 


namespace KuaforWebSitesi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VerimlilikController : ControllerBase
    {
        private readonly KuaforDBContext db;
        private readonly HttpClient _httpClient;
        public VerimlilikController(KuaforDBContext context)
        {
            db = context;
        }

        [HttpGet]     
        public IActionResult TumCalisanlarVerimlilik()
        {
            // Tüm çalışanları al
            var calisanlar = db.Calisanlar.ToList();

            var sonucListesi = new List<object>();

            foreach (var calisan in calisanlar)
            {
                var calisanId = calisan.CalisanID;

                var calisanHizmetSuresi = (from r in db.Randevular
                                           join h in db.Hizmetler on r.HizmetID equals h.HizmetID
                                           where r.CalisanID == calisanId && h.Sure != null
                                           select h.Sure.TotalMinutes)
                                    .AsEnumerable()  
                                    .Sum();

              
                if (calisanHizmetSuresi == 0)
                {
                    calisanHizmetSuresi = 0;
                }


                // Tüm salonun toplam hizmet süresi
                var toplamHizmetSuresi = (from r in db.Randevular
                                          join h in db.Hizmetler on r.HizmetID equals h.HizmetID
                                          where h.Sure != null
                                          select h.Sure.TotalMinutes)
                                  .AsEnumerable() 
                                  .Sum();

                // Eğer toplam hizmet süresi 0 ise, sıfır kabul et
                if (toplamHizmetSuresi == 0)
                {
                    toplamHizmetSuresi = 0;
                }


                // Çalışanın yaptığı hizmet sayısı
                var calisanHizmetSayisi = db.Randevular.Count(r => r.CalisanID == calisanId);

                // Tüm salonun toplam hizmet sayısı
                var toplamHizmetSayisi = db.Randevular.Count();

                // Verimlilik oranını hesapla
                double verimlilikOrani = 0;
                if (toplamHizmetSuresi > 0 && toplamHizmetSayisi > 0)
                {
                    var hizmetSuresiOrani = (double)calisanHizmetSuresi / toplamHizmetSuresi;
                    var hizmetSayisiOrani = (double)calisanHizmetSayisi / toplamHizmetSayisi;

                    verimlilikOrani = (hizmetSuresiOrani + hizmetSayisiOrani) / 2 * 100;
                }

                // Çalışanın verimlilik oranı ile birlikte sonucu ekle
                sonucListesi.Add(new
                {
                    CalisanID = calisanId,
                    CalisanAd = calisan.CalisanAd,
                    CalisanSoyad = calisan.CalisanSoyad,
                    VerimlilikOrani = verimlilikOrani
                });
            }

            // Sonuçları döndür
            return Ok(sonucListesi);
        }


    }
}
