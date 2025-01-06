using System.ComponentModel.DataAnnotations;

namespace KuaforWebSitesi.Models
{
    public class Iletisim
    {
        [Key]
        public int IletisimId { get; set; }  
        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public string Ad { get; set; }
        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Eposta { get; set; }
        [Required(ErrorMessage = "Mesaj alanı zorunludur.")]
        public string Mesaj { get; set; }
    }
}
