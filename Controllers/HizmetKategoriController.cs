using KuaforWebSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Linq;
using static System.Web.Razor.Parser.SyntaxConstants;
using System.Security.Cryptography;
using System.Text;


namespace KuaforWebSitesi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HizmetKategoriController : ControllerBase
    {
        private readonly KuaforDBContext db;

        public HizmetKategoriController(KuaforDBContext context)
        {
            db = context;
        }
        // GET: api/HizmetKategori
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HizmetKategori>>> GetHizmetKategoriler()
        {
            var kategoriler = await db.HizmetKategoriler.Include(h => h.Hizmetler).ToListAsync();
            return Ok(kategoriler);
        }

        // GET: api/HizmetKategori/{id}
        //[HttpGet("{id}")]
        //public async Task<ActionResult<HizmetKategori>> GetHizmetKategori(int id)
        //{
        //    var kategori = await db.HizmetKategoriler.Include(h => h.Hizmetler).FirstOrDefaultAsync(h => h.HizmetKategoriID == id);

        //    if (kategori == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(kategori);
        //}


        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<HizmetKategori>>> GetHizmetKategoriler()
        //{
        //    var kategoriler = await db.HizmetKategoriler
        //        .Include(h => h.Hizmetler)
        //        .ToListAsync();

        //    var kategoriDtos = kategoriler.Select(k => new HizmetKategori
        //    {
        //        HizmetKategoriID = k.HizmetKategoriID,
        //        KategoriAdi = k.KategoriAdi,
        //        HizmetAdlari = k.Hizmetler?.Select(h => h.HizmetAdi).ToList()  // Hizmet adlarını al
        //    }).ToList();

        //    return Ok(kategoriDtos);
        //}


        // POST: api/HizmetKategori/AddMultipleKategoriler
        [HttpPost("AddMultipleKategoriler")]
        public async Task<ActionResult> AddMultipleKategoriler()
        {
            try
            {
                // Kategorileri bir liste halinde tanımlayalım
                var kategoriler = new List<HizmetKategori>
                {
                    new HizmetKategori { KategoriAdi = "Saç Bakımı" },
                    new HizmetKategori { KategoriAdi = "Saç Şekillenidirme" },
                    new HizmetKategori { KategoriAdi = "Gelin" },
                    new HizmetKategori { KategoriAdi = "Manikür & Pedikür" },
                    new HizmetKategori { KategoriAdi = "Cilt" }
                };

                // Veritabanına kategorileri ekliyoruz
                await db.HizmetKategoriler.AddRangeAsync(kategoriler);
                await db.SaveChangesAsync();

                return Ok(new { message = "Kategoriler başarıyla eklendi!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Bir hata oluştu: " + ex.Message });
            }
        }

        // GET: api/HizmetKategori
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<HizmetKategori>>> GetHizmetKategoriler()
        //{
        //    return await db.HizmetKategoriler.ToListAsync();
        //}

        // GET: api/HizmetKategori/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HizmetKategori>> GetHizmetKategori(int id)
        {
            var hizmetKategori = await db.HizmetKategoriler.FindAsync(id);

            if (hizmetKategori == null)
            {
                return NotFound();
            }

            return hizmetKategori;
        }

        // POST: api/HizmetKategori
        [HttpPost]
        public async Task<ActionResult<HizmetKategori>> PostHizmetKategori(HizmetKategori hizmetKategori)
        {
            db.HizmetKategoriler.Add(hizmetKategori);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHizmetKategori), new { id = hizmetKategori.HizmetKategoriID }, hizmetKategori);
        }

        // PUT: api/HizmetKategori/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHizmetKategori(int id, HizmetKategori hizmetKategori)
        {
            if (id != hizmetKategori.HizmetKategoriID)
            {
                return BadRequest();
            }

            db.Entry(hizmetKategori).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HizmetKategoriExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/HizmetKategori/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHizmetKategori(int id)
        {
            var hizmetKategori = await db.HizmetKategoriler.FindAsync(id);
            if (hizmetKategori == null)
            {
                return NotFound();
            }

            db.HizmetKategoriler.Remove(hizmetKategori);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool HizmetKategoriExists(int id)
        {
            return db.HizmetKategoriler.Any(e => e.HizmetKategoriID == id);
        }
    }
}
