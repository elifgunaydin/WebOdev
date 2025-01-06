using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KuaforWebSitesi.Models
{
    public class Gunler
    {
        [Key]
        public int GunID { get; set; }

        [Required]
        public string GunAdi { get; set; }
        public virtual ICollection<CalisanGun> CalisanGunler { get; set; } 
        //public virtual ICollection<CalisanSaat> CalisanSaatler { get; set; } // Hangi çalışanlar hangi saatlerde çalışıyor
    }
}
