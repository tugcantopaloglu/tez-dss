using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonerSermaye.Models.Data;
using MailKit.Net.Smtp;
using MimeKit;

namespace DonerSermaye.Controllers
{
    public class DuyuruController : Controller
    {
        private readonly donersermayeContext _context;

        public DuyuruController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: Duyuru
        public async Task<IActionResult> Index()
        {
            var donersermayeContext = _context.Duyurular.Include(d => d.GonderenNavigation);

            return View(await donersermayeContext.ToListAsync());
        }

        // GET: Duyuru/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duyurular = await _context.Duyurular
                .Include(d => d.GonderenNavigation)
                .SingleOrDefaultAsync(m => m.DuyuruNo == id);
            if (duyurular == null)
            {
                return NotFound();
            }

            return View(duyurular);
        }

        // GET: Duyuru/Create
        public IActionResult Create()
        {
            ViewData["Gonderen"] = new SelectList(_context.Personeller, "Id", "Id");
            return View();
        }

        // POST: Duyuru/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DuyuruNo,Baslik,Duyuru,Tarih,Gonderen")] Duyurular duyurular)
        {
            if (ModelState.IsValid)
            {
                _context.Add(duyurular);
                await _context.SaveChangesAsync();

                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress("Mühendislik Fakültesi Döner Sermaye Sistemi",
                "muhdss@mail.ege.edu.tr");
                message.From.Add(from);

                var users = _context.Personeller.Where(a => a.EPosta != null && a.EPosta != "" && a.EPosta == "erkan.yildirim@ege.edu.tr").ToList();

                MailboxAddress to = new MailboxAddress("DSS",
                        "muhdss@mail.ege.edu.tr");

                message.To.Add(to);

                to = new MailboxAddress("Yusuf Balcı",
                        "yusuf.balci@ege.edu.tr");

                message.To.Add(to);

                foreach (var us in users)
                {
                    to = new MailboxAddress(us.Ad + " " + us.Soyad,
                        us.EPosta);
                    message.Bcc.Add(to);
                }                

                message.Subject = duyurular.Baslik;

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "<h1>" + duyurular.Baslik + "</h1>";
                bodyBuilder.TextBody = duyurular.Duyuru;

                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                client.Connect("mail.ege.edu.tr", 587, true);
                client.Authenticate("muhdss@mail.ege.edu.tr", "2021.egeM");

                client.Send(message);
                client.Disconnect(true);
                client.Dispose();

                return RedirectToAction(nameof(Index));
            }
            ViewData["Gonderen"] = new SelectList(_context.Personeller, "Id", "Id", duyurular.Gonderen);
            return View(duyurular);
        }

        // GET: Duyuru/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duyurular = await _context.Duyurular.SingleOrDefaultAsync(m => m.DuyuruNo == id);
            if (duyurular == null)
            {
                return NotFound();
            }
            ViewData["Gonderen"] = new SelectList(_context.Personeller, "Id", "Id", duyurular.Gonderen);
            return View(duyurular);
        }

        // POST: Duyuru/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DuyuruNo,Baslik,Duyuru,Tarih,Gonderen")] Duyurular duyurular)
        {
            if (id != duyurular.DuyuruNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(duyurular);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DuyurularExists(duyurular.DuyuruNo))
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
            ViewData["Gonderen"] = new SelectList(_context.Personeller, "Id", "Id", duyurular.Gonderen);
            return View(duyurular);
        }

        // GET: Duyuru/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duyurular = await _context.Duyurular
                .Include(d => d.GonderenNavigation)
                .SingleOrDefaultAsync(m => m.DuyuruNo == id);
            if (duyurular == null)
            {
                return NotFound();
            }

            return View(duyurular);
        }

        // POST: Duyuru/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var duyurular = await _context.Duyurular.SingleOrDefaultAsync(m => m.DuyuruNo == id);
            _context.Duyurular.Remove(duyurular);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DuyurularExists(int id)
        {
            return _context.Duyurular.Any(e => e.DuyuruNo == id);
        }
    }
}
