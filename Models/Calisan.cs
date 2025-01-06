using System.ComponentModel.DataAnnotations;

namespace KuaforWebSitesi.Models
{
    public class Calisan
    {
        [Key]
        public int CalisanID { get; set; }

        [Required]
        public string CalisanAd { get; set; }

        [Required]
        public string CalisanSoyad { get; set; }

        [Required]
        [EmailAddress]
        public string CalisanMail { get; set; }

        [Required]
        [Phone]
        public string CalisanTelefon { get; set; }

        [Required]
        public string CalisanSifre { get; set; }
    
        public ICollection<CalisanHizmetler>? CalisanHizmetler { get; set; }
        public ICollection<Randevu>? Randevular { get; set; } = new List<Randevu>();
        public ICollection<CalisanGun>? CalisanGunler { get; set; }

        //public int RolID { get; set; } // Role ID (Roles tablosuna referans)
        //public Role Role { get; set; } // Navigation property

    }
}