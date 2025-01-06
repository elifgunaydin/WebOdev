
using System.ComponentModel.DataAnnotations;

namespace KuaforWebSitesi.Models
{
    public class Randevu
    {
        [Key]
        public int RandevuID { get; set; }

        [Required(ErrorMessage = "Hizmet seçmek zorunludur.")]

        public int HizmetID { get; set; }

        [Required(ErrorMessage = "Çalışan seçmek zorunludur.")]
        public int CalisanID { get; set; }

        [Required(ErrorMessage = "Müşteri seçmek zorunludur.")]
        public int MusteriID { get; set; }

        [Required]
        public DateTime Tarih { get; set; }

        [Required]
        public TimeSpan Saat { get; set; }
        public string Durum { get; set; } = "Bekliyor"; 
      

        public virtual Musteri Musteri { get; set; }
        public virtual Calisan Calisan { get; set; }
        public virtual Hizmetler Hizmetler { get; set; }

    }
    public enum RandevuDurumu
    {
        Bekliyor,
        Onaylandı,
        Iptal
    }

}
