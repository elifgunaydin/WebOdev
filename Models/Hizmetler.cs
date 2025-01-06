using System.ComponentModel.DataAnnotations;

namespace KuaforWebSitesi.Models
{
    public class Hizmetler
    {
        [Key]
        public int HizmetID { get; set; }

        [Required]
        public string HizmetAdi { get; set; } = null!;

        [Required]
        public TimeSpan Sure { get; set; } 

        [Required]
        public decimal Fiyat { get; set; } 
        public int? HizmetKategoriID { get; set; }
        public HizmetKategori HizmetKategoriler { get; set; } = null!;
      


        public ICollection<Randevu>? Randevular { get; set; }

        public ICollection<CalisanHizmetler>? CalisanHizmetler { get; set; }
    }
}
