using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KuaforWebSitesi.Models
{
    public class CalisanGun
    {
        [Key]
        public int CalisanGunID { get; set; }
        public int CalisanID { get; set; }
        public  Calisan? Calisanlar { get; set; }
        public TimeSpan BaslangicSaati { get; set; }
        public TimeSpan BitisSaati { get; set; }

        public int GunID { get; set; }
        public  Gunler? Gunler { get; set; }
    }
}
