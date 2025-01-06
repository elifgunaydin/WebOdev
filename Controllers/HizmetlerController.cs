using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Collections.Specialized.BitVector32;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Threading;
using System;

namespace KuaforWebSitesi.Controllers
{
    public class HizmetlerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
