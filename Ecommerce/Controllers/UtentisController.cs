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
    public class UtentisController : Controller
    {
        private readonly EcommerceContext _context;

        public UtentisController(EcommerceContext context)
        {
            _context = context;
        }

        // GET: Utentis
        public async Task<IActionResult> Index()
        {
              return _context.Utentis != null ? 
                          View(await _context.Utentis.ToListAsync()) :
                          Problem("Entity set 'EcommerceContext.Utentis'  is null.");
        }

        // GET: Utentis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Utentis == null)
            {
                return NotFound();
            }

            var utenti = await _context.Utentis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utenti == null)
            {
                return NotFound();
            }

            return View(utenti);
        }

        // GET: Utentis/Create
        public IActionResult Create(int id)
        {
			ViewBag.NuovoUtenteID = _context.Utentis.Max(c => c.Id) + 1;
			return View();
        }

        // POST: Utentis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Cognome")] Utenti utenti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utenti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(utenti);
        }

        // GET: Utentis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Utentis == null)
            {
                return NotFound();
            }

            var utenti = await _context.Utentis.FindAsync(id);
            if (utenti == null)
            {
                return NotFound();
            }
            return View(utenti);
        }

        // POST: Utentis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Cognome")] Utenti utenti)
        {
            if (id != utenti.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utenti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtentiExists(utenti.Id))
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
            return View(utenti);
        }

        // GET: Utentis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Utentis == null)
            {
                return NotFound();
            }

            var utenti = await _context.Utentis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utenti == null)
            {
                return NotFound();
            }

            return View(utenti);
        }

        // POST: Utentis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Utentis == null)
            {
                return Problem("Entity set 'EcommerceContext.Utentis'  is null.");
            }
            var utenti = await _context.Utentis.FindAsync(id);
            if (utenti != null)
            {
                _context.Utentis.Remove(utenti);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtentiExists(int id)
        {
          return (_context.Utentis?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
