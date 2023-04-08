using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonerSermaye.Models.Data;
using Microsoft.AspNetCore.Http;
using DonerSermaye.Models.Service;

namespace DonerSermaye.Controllers
{
    public class GenelDurumRaporuController : Controller
    {
        private readonly donersermayeContext _context;

        public GenelDurumRaporuController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: IstekGir
        public async Task<IActionResult> Index()
        {
            var m = new List<FakulteGenelDurumViewModel>();

            // todo: yıl eklenecek. şimdilik hepsini veriyor.
            var zz = from a in _context.Bolumler select new FakulteGenelDurumViewModel { Id= a.Id, Bolum = a.Bolum };


            if (HttpContext.Session.GetString("yetki") == "1")
            {
                zz = zz.Where(a => a.Id == Convert.ToInt32(HttpContext.Session.GetString("bolumId")));
            }

            GenelDurumService genelDurum = new GenelDurumService(_context);
            foreach (var z in zz)
            {
                var fgm = genelDurum.Bolum(z.Id);
                m.Add(fgm);
            }

            return View(m);
        }

    }
}
