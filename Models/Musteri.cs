using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KuaforWebSitesi.Models
{
    [Index(nameof(MusteriMail), IsUnique = true)]
    [Index(nameof(MusteriTelefon), IsUnique = true)]
    public class Musteri
    {
        [Key]
        public int MusteriID { get; set; }



        [Required(ErrorMessage = "Lütfen Adınızı Giriniz")]
        [MinLength(3, ErrorMessage = "Ad alanı minimum 3 karakter olmalı")]
        [MaxLength(20, ErrorMessage = "Ad alanı Makismum 20 karakter olmalı")]
        public string MusteriAd { get; set; }



        [Required(ErrorMessage = "Lütfen Soyadınızı Giriniz:")]
        public string MusteriSoyad { get; set; }



        [Required(ErrorMessage = "Lütfen E-Mailinizi Giriniz.")]
        [EmailAddress(ErrorMessage = "Mail adresi uygun değil")]
        public string MusteriMail { get; set; }




        [Required(ErrorMessage = "Lütfen Telefon Numarasını Giriniz")]
        [RegularExpression(@"^(\+90|0)?[1-9][0-9]{9}$", ErrorMessage = "Geçerli bir telefon numarası giriniz")]

        public string MusteriTelefon { get; set; }



        [Required(ErrorMessage = "Lütfen şifre oluşturunuz.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        [RegularExpression(@"^(?=.*[A-ZİĞÜŞÇÖ])(?=.*\d)[A-Za-zİıĞğÜüŞşÇçÖö\d@$!%*?&]{6,}$",
        ErrorMessage = "Şifre en az 6 karakter olmalı, en az 1 büyük harf ve en az 1 rakam içermelidir.")]
        public string MusteriSifre { get; set; }


        public virtual ICollection<MusteriRol> MusteriRoller { get; set; }

        public Musteri()
        {
            MusteriRoller = new List<MusteriRol>(); // Initialize to avoid null reference errors
        }

        //[Required(ErrorMessage = "Şifreyi tekrar giriniz.")]
        //[Compare("MusteriSifre", ErrorMessage = "Şifreler eşleşmiyor.")]
        //public string MusteriSifreTekrar { get; set; }
        public virtual ICollection<Randevu> Randevular { get; set; } = new List<Randevu>();
    }
}
