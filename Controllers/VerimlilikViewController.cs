using KuaforWebSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace KuaforWebSitesi.Controllers
{
    public class VerimlilikViewController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly KuaforDBContext db;
        public VerimlilikViewController(HttpClient httpClient)
        {
            _httpClient = httpClient;  
        }

        public async Task<IActionResult> TumCalisanlarVerimlilik()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://localhost:7035/api/Verimlilik");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var sonucListesi = JsonConvert.DeserializeObject<List<CalisanVerimlilik>>(jsonData);  
                    return View(sonucListesi);  
                }
                return View("Error");  
            }
            catch (Exception ex)
            {                
                return View("Error", ex.Message);
            }
        }


    }
}
