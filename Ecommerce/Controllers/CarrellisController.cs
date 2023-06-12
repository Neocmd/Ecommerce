using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    public class CarrellisController : Controller
    {
        private readonly EcommerceContext _context;

        public CarrellisController(EcommerceContext context)
        {
            _context = context;
        }

        // GET: Carrellis
        public async Task<IActionResult> Index()
        {
            var ecommerceContext = _context.Carrellis.Include(c => c.Utente);
            return View(await ecommerceContext.ToListAsync());
        }

        // GET: Carrellis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Carrellis == null)
            {
                return NotFound();
            }

            var carrelli = await _context.Carrellis
                .Include(c => c.Utente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrelli == null)
            {
                return NotFound();
            }

            return View(carrelli);
        }

        // GET: Carrellis/Create
        public IActionResult Create()
        {
            ViewBag.NuovoCarrelloID = _context.Carrellis.Any() ? _context.Carrellis.Max(c => c.Id + 1) : 0; 
            ViewData["UtenteId"] = new SelectList(_context.Utentis, "Id", "Cognome");
            return View();
        }

        // POST: Carrellis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UtenteId")] Carrelli carrelli)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carrelli);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UtenteId"] = new SelectList(_context.Utentis, "Id", "Cognome", carrelli.UtenteId);
            return View(carrelli);
        }

        // GET: Carrellis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Carrellis == null)
            {
                return NotFound();
            }

            var carrelli = await _context.Carrellis.FindAsync(id);
            if (carrelli == null)
            {
                return NotFound();
            }
            ViewData["UtenteId"] = new SelectList(_context.Utentis, "Id", "Cognome", carrelli.UtenteId);
            return View(carrelli);
        }

        // POST: Carrellis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UtenteId")] Carrelli carrelli)
        {
            if (id != carrelli.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrelli);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarrelliExists(carrelli.Id))
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
            ViewData["UtenteId"] = new SelectList(_context.Utentis, "Id", "Cognome", carrelli.UtenteId);
            return View(carrelli);
        }

        // GET: Carrellis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Carrellis == null)
            {
                return NotFound();
            }

            var carrelli = await _context.Carrellis
                .Include(c => c.Utente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrelli == null)
            {
                return NotFound();
            }

            return View(carrelli);
        }

        // POST: Carrellis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carrellis == null)
            {
                return Problem("Entity set 'EcommerceContext.Carrellis'  is null.");
            }
            var carrelli = await _context.Carrellis.FindAsync(id);
            if (carrelli != null)
            {
                _context.Carrellis.Remove(carrelli);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarrelliExists(int id)
        {
          return (_context.Carrellis?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
