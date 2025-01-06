using KuaforWebSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KuaforWebSitesi.Controllers
{
    public class IletisimViewController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly KuaforDBContext db;
        public IletisimViewController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

       
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MesajGoster()
        {
            List<Iletisim> mesajlar = new List<Iletisim>();

            try
            {
                // IHttpClientFactory ile HttpClient nesnesi alıyoruz
                var client = _httpClientFactory.CreateClient();

                // API'ye GET isteği gönderiyoruz
                var response = await client.GetAsync("https://localhost:7035/api/Iletisim");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    mesajlar = JsonConvert.DeserializeObject<List<Iletisim>>(jsonString);
                }
                else
                {
                    TempData["Message"] = "Mesajlar alınırken bir hata oluştu.";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Bir hata oluştu: {ex.Message}";
            }

            return View(mesajlar);
        }
     
        [HttpPost]
        public async Task<IActionResult> MesajGonder(Iletisim model)
        {
            if (ModelState.IsValid)
            {
                
                var client = _httpClientFactory.CreateClient();

               
                var content = new StringContent(
                    JsonConvert.SerializeObject(model),
                    Encoding.UTF8,
                    "application/json"
                );

                try
                {
                  
                    var response = await client.PostAsync("https://localhost:7035/api/Iletisim", content);

                    if (response.IsSuccessStatusCode)
                    {
                       
                        ViewBag.Message = "Mesajınız başarıyla alındı, teşekkür ederiz.";
                    }
                    else
                    {
                      
                        ViewBag.Message = "Bir hata oluştu, lütfen tekrar deneyin.";
                    }
                }
                catch (Exception ex)
                {
                   
                    ViewBag.Message = $"Bir hata oluştu: {ex.Message}";
                }
            }
            else
            {
              
                ViewBag.Message = "Formda eksik veya hatalı alanlar bulunuyor.";
            }

            return View("Index");
        }
       

    }

   

}
