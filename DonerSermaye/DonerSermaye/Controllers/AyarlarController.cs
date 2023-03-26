using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonerSermaye.Models.Data;
using System.Globalization;

namespace DonerSermaye.Controllers
{
    public class AyarlarController : Controller
    {
        private readonly donersermayeContext _context;

        public AyarlarController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: Odenen/Edit/5
        public async Task<IActionResult> Edit()
        {
            var ayarlar = await _context.Ayarlar.SingleOrDefaultAsync(m => m.Id == 1);
            if (ayarlar == null)
            {
                return NotFound();
            }
            return View(ayarlar);
        }

        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("BSBALimiti,Dekan,Raportor,Uye1,Uye2,Uye3,Uye4,Uye5,Uye6")] Ayarlar ayarlar)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ayarlar.Id = 1;
                    _context.Update(ayarlar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return View(ayarlar);
        }        
    }
}
