namespace KuaforWebSitesi.Models
{
    public class Rol
    {
        public int RolID { get; set; } = 2;
        public string RolAdi { get; set; }
        public virtual ICollection<MusteriRol> MusteriRoller { get; set; }

    }

}
