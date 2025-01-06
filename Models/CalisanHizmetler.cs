using System.ComponentModel.DataAnnotations;

namespace KuaforWebSitesi.Models
{
    public class CalisanHizmetler
    {
        [Key]
        public int CalisanHizmetID { get; set; }


        public int CalisanID { get; set; }
        public Calisan? Calisan { get; set; }

      
        public int HizmetID { get; set; }
        public Hizmetler? Hizmet { get; set; }


    }
}
