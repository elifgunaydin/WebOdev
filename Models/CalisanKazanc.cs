namespace KuaforWebSitesi.Models
{
    public class CalisanKazanc
    {
        public int CalisanID { get; set; }
        public string CalisanAd { get; set; }
        public string CalisanSoyad { get; set; }
        public double ToplamKazanc { get; set; } // Aylık toplam kazanç
        public double GunlukKazanc { get; set; } // Günlük kazanç
    }
}
