using KuaforWebSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KuaforWebSitesi.Models
{

    public class HizmetKategori
    {
        public int HizmetKategoriID { get; set; } 
        public string KategoriAdi { get; set; }   

        public virtual ICollection<Hizmetler>? Hizmetler { get; set; }  
    }
}



