using KuaforWebSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

//https://localhost:7035/api/Iletisim


//DELETE https://localhost:7035/api/Iletisim/2 !delete yapmayı unutma postman


namespace KuaforWebSitesi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IletisimController : ControllerBase
    {
        private readonly KuaforDBContext db;

        public IletisimController(KuaforDBContext context)
        {
            db = context;
        }

        // POST: api/iletisim
        [HttpPost]
        public async Task<IActionResult> MesajGonder([FromBody] Iletisim model)
        {
            // ModelState geçersizse, hatalı istek gönderildiğini belirtiyoruz
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // E-posta doğrulaması yapıyoruz
            if (!IsValidEmail(model.Eposta))
            {
                return BadRequest(new { message = "Geçersiz e-posta adresi." });
            }

            try
            {
              
                await db.Iletisimler.AddAsync(model);
                await db.SaveChangesAsync();

                return Ok(new { message = "Mesajınız başarıyla alındı, teşekkür ederiz." });
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Hata: {ex.Message}");
                return StatusCode(500, new { message = "Bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }

       
        private bool IsValidEmail(string email)
        {
           
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }

        [HttpGet]
        public IActionResult GetMesajlar()
        {
            try
            {
                var mesajlar = db.Iletisimler.ToList();  

                if (mesajlar == null || mesajlar.Count == 0)
                {
                    return NotFound(new { message = "Hiç mesaj bulunmamaktadır." });
                }

                return Ok(mesajlar);  
            }
            catch (Exception ex)
            {
             
                Console.WriteLine($"Hata: {ex.Message}");
                return StatusCode(500, new { message = "Mesajlar alınırken bir hata oluştu." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMesaj(int id)
        {
            Console.WriteLine($"Received DELETE request for ID: {id}");

            try
            {
               
                Console.WriteLine($"Attempting to delete message with ID: {id}");

                var iletisim = await db.Iletisimler.FirstOrDefaultAsync(x => x.IletisimId == id);

              
                if (iletisim == null)
                {
                    Console.WriteLine($"Mesaj ID {id} bulunamadı.");
                    return NotFound(new { message = "Mesaj bulunamadı." });
                }

                db.Iletisimler.Remove(iletisim);
                await db.SaveChangesAsync();

                
                Console.WriteLine($"Mesaj ID {id} başarıyla silindi.");
                return Ok(new { message = "Mesaj başarıyla silindi." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");
                return StatusCode(500, new { message = "Bir hata oluştu: " + ex.Message });
            }
        }


    }
}


