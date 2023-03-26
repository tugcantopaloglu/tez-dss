using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonerSermaye.Models.Data;

namespace DonerSermaye.Controllers
{
    public class KesintiOranController : Controller
    {
        private readonly donersermayeContext _context;

        public KesintiOranController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: KesintiOran
        public async Task<IActionResult> Index()
        {
            var donersermayeContext = _context.KesintiOran.Include(x => x.Tur).Include(y => y.KesintiTip).ThenInclude(z => z.kesintiTipAyrinti);
            return View(await donersermayeContext.ToListAsync());
        }

        // GET: KesintiOran/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kesintiOran = await _context.KesintiOran
                .SingleOrDefaultAsync(m => m.Id == id);
            if (kesintiOran == null)
            {
                return NotFound();
            }

            return View(kesintiOran);
        }

        // GET: KesintiOran/Create
        public IActionResult Create()
        {
            ViewData["Tur"] = new SelectList(_context.IsTurleri, "Id", "Tur");
            ViewData["KesintiTipi"] = new SelectList(_context.Kesintitipleri, "Id", "KesintiTipi");
            return View();
        }

        // POST: KesintiOran/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KesintiTipId,TurId,Oran")] KesintiOran kesintiOran)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kesintiOran);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(kesintiOran);
        }

        // GET: KesintiOran/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kesintiOran = await _context.KesintiOran.SingleOrDefaultAsync(m => m.Id == id);

            ViewData["Tur"] = new SelectList(_context.IsTurleri, "Id", "Tur", kesintiOran.Tur);
            ViewData["KesintiTipi"] = new SelectList(_context.Kesintitipleri, "Id", "KesintiTipi", kesintiOran.KesintiTip);
            if (kesintiOran == null)
            {
                return NotFound();
            }
            return View(kesintiOran);
        }

        // POST: KesintiOran/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KesintiTipId,TurId,Oran")] KesintiOran kesintiOran)
        {
            if (id != kesintiOran.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kesintiOran);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KesintiOranExists(kesintiOran.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(kesintiOran);
        }

        // GET: KesintiOran/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kesintiOran = await _context.KesintiOran
                .SingleOrDefaultAsync(m => m.Id == id);
            if (kesintiOran == null)
            {
                return NotFound();
            }

            return View(kesintiOran);
        }

        // POST: KesintiOran/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kesintiOran = await _context.KesintiOran.SingleOrDefaultAsync(m => m.Id == id);
            _context.KesintiOran.Remove(kesintiOran);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KesintiOranExists(int id)
        {
            return _context.KesintiOran.Any(e => e.Id == id);
        }
    }
}
