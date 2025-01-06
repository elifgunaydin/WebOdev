namespace KuaforWebSitesi.Models
{
    public class MusteriRol
    {
        public int MusteriRolID {get;set;}
        public int MusteriID { get; set; }
        public Musteri Musteri { get; set; }

        public int RolID { get; set; } 
        public Rol Rol { get; set; }
        public MusteriRol(int rolID = 2) 
        {
            RolID = rolID;
        }
    }
}
