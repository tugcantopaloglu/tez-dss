using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonerSermaye.Models.Data;
using Microsoft.AspNetCore.Http;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Security.Authentication;

namespace DonerSermaye.Controllers
{
    public class DuyurularController : Controller
    {
        private readonly donersermayeContext _context;

        public DuyurularController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: Duyurular
        public async Task<IActionResult> Index()
        {
            var donersermayeContext = _context.Duyurular.Include(d => d.GonderenNavigation);
            return View(await donersermayeContext.ToListAsync());
        }

        // GET: Duyurular/Details/5
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

        // GET: Duyurular/Create
        public IActionResult Create()
        {
            ViewData["Gonderen"] = new SelectList(_context.Personeller, "Id", "Id");
            return View();
        }

        // POST: Duyurular/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DuyuruNo,Baslik,Duyuru,Tarih,Gonderen")] Duyurular duyurular)
        {
            duyurular.Tarih = DateTime.Now;
            duyurular.Gonderen =Convert.ToInt32(HttpContext.Session.GetString("Id"));
            if (ModelState.IsValid)
            {
                
                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress("Mühendislik Fakültesi Döner Sermaye Sistemi",
                "muhdss@mail.ege.edu.tr");
                message.From.Add(from);

                var users = _context.Personeller.Where(a => a.EPosta != null && a.EPosta != "").ToList();

                MailboxAddress to = new MailboxAddress("DSS",
                        "muhdss@mail.ege.edu.tr");

                message.To.Add(to);

                foreach (var us in users)
                {
                    to = new MailboxAddress(us.Ad + " " + us.Soyad,
                        us.EPosta);
                    message.Bcc.Add(to);
                }

                message.Subject = duyurular.Baslik;

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "<p>" + duyurular.Duyuru + "</p>";

                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.CheckCertificateRevocation = false;
                client.SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;

                client.Connect("mail.ege.edu.tr", 587, SecureSocketOptions.StartTlsWhenAvailable);
                client.Authenticate("muhdss@mail.ege.edu.tr", "2021.egeM");

                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
                _context.Add(duyurular);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Gonderen"] = new SelectList(_context.Personeller, "Id", "Id", duyurular.Gonderen);
            return View(duyurular);
        }

        // GET: Duyurular/Edit/5
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

        // POST: Duyurular/Edit/5
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

        // GET: Duyurular/Delete/5
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

        // POST: Duyurular/Delete/5
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
