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
    public class CarrelloProdottisController : Controller
    {
        private readonly EcommerceContext _context;

        public CarrelloProdottisController(EcommerceContext context)
        {
            _context = context;
        }

        // GET: CarrelloProdottis
        public async Task<IActionResult> Index()
        {
            var ecommerceContext = _context.CarrelloProdottis.Include(c => c.Carrello).Include(c => c.Prodotto);
            return View(await ecommerceContext.ToListAsync());
        }

        // GET: CarrelloProdottis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarrelloProdottis == null)
            {
                return NotFound();
            }

            var carrelloProdotti = await _context.CarrelloProdottis
                .Include(c => c.Carrello)
                .Include(c => c.Prodotto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrelloProdotti == null)
            {
                return NotFound();
            }

            return View(carrelloProdotti);
        }

        // GET: CarrelloProdottis/Create
        public IActionResult Create()
        {
            ViewData["CarrelloId"] = new SelectList(_context.Carrellis, "Id", "Id");
            ViewData["ProdottoId"] = new SelectList(_context.Prodottis, "Id", "Nome");
            return View();
        }

        // POST: CarrelloProdottis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarrelloId,ProdottoId")] CarrelloProdotti carrelloProdotti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carrelloProdotti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarrelloId"] = new SelectList(_context.Carrellis, "Id", "Id", carrelloProdotti.CarrelloId);
            ViewData["ProdottoId"] = new SelectList(_context.Prodottis, "Id", "Nome", carrelloProdotti.ProdottoId);
            return View(carrelloProdotti);
        }

        // GET: CarrelloProdottis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarrelloProdottis == null)
            {
                return NotFound();
            }

            var carrelloProdotti = await _context.CarrelloProdottis.FindAsync(id);
            if (carrelloProdotti == null)
            {
                return NotFound();
            }
            ViewData["CarrelloId"] = new SelectList(_context.Carrellis, "Id", "Id", carrelloProdotti.CarrelloId);
            ViewData["ProdottoId"] = new SelectList(_context.Prodottis, "Id", "Nome", carrelloProdotti.ProdottoId);
            return View(carrelloProdotti);
        }

        // POST: CarrelloProdottis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarrelloId,ProdottoId")] CarrelloProdotti carrelloProdotti)
        {
            if (id != carrelloProdotti.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrelloProdotti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarrelloProdottiExists(carrelloProdotti.Id))
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
            ViewData["CarrelloId"] = new SelectList(_context.Carrellis, "Id", "Id", carrelloProdotti.CarrelloId);
            ViewData["ProdottoId"] = new SelectList(_context.Prodottis, "Id", "Nome", carrelloProdotti.ProdottoId);
            return View(carrelloProdotti);
        }

        // GET: CarrelloProdottis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarrelloProdottis == null)
            {
                return NotFound();
            }

            var carrelloProdotti = await _context.CarrelloProdottis
                .Include(c => c.Carrello)
                .Include(c => c.Prodotto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrelloProdotti == null)
            {
                return NotFound();
            }

            return View(carrelloProdotti);
        }

        // POST: CarrelloProdottis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarrelloProdottis == null)
            {
                return Problem("Entity set 'EcommerceContext.CarrelloProdottis'  is null.");
            }
            var carrelloProdotti = await _context.CarrelloProdottis.FindAsync(id);
            if (carrelloProdotti != null)
            {
                _context.CarrelloProdottis.Remove(carrelloProdotti);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarrelloProdottiExists(int id)
        {
          return (_context.CarrelloProdottis?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
