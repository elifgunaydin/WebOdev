using KuaforWebSitesi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace KuaforWebSitesi.Controllers
{
    [Authorize(Roles = "Admin")] 
    public class AdminController : Controller
    {
        private readonly KuaforDBContext db;
        private readonly PasswordHasher<Musteri> _passwordHasher;
        private readonly ILogger<MusteriController> _logger;
        public AdminController(KuaforDBContext context, PasswordHasher<Musteri> passwordHasher, ILogger<MusteriController> logger)
        {
            db = context;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }
        // Admin Paneli sayfasına yönlendiren bir action metodu
        [HttpGet]
        public IActionResult AdminPaneli()
        {
            var kullaniciRol = HttpContext.Session.GetString("RolAdi");

            if (kullaniciRol != "Admin")
            {
                return RedirectToAction("AccessDenied", "Admin");
            }

            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

     
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Index", "Home");
        }

       



    }
}
