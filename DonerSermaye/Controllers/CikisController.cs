using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DonerSermaye.Controllers
{
    public class CikisController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            Redirect("/Giris");
            return View();
        }
    }
}