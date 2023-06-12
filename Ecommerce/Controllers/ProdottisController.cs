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
    public class ProdottisController : Controller
    {
        private readonly EcommerceContext _context;

        public ProdottisController(EcommerceContext context)
        {
            _context = context;
        }

        // GET: Prodottis
        public async Task<IActionResult> Index()
        {
              return _context.Prodottis != null ? 
                          View(await _context.Prodottis.ToListAsync()) :
                          Problem("Entity set 'EcommerceContext.Prodottis'  is null.");
        }

        // GET: Prodottis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prodottis == null)
            {
                return NotFound();
            }

            var prodotti = await _context.Prodottis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prodotti == null)
            {
                return NotFound();
            }

            return View(prodotti);
        }

        // GET: Prodottis/Create
        public IActionResult Create(int id)
        {
            ViewBag.NuovoProdottoID = _context.Prodottis.Max(c => c.Id) + 1;
            return View();
        }

        // POST: Prodottis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Prezzo,QuantitaDisponibile")] Prodotti prodotti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prodotti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prodotti);
        }

        // GET: Prodottis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prodottis == null)
            {
                return NotFound();
            }

            var prodotti = await _context.Prodottis.FindAsync(id);
            if (prodotti == null)
            {
                return NotFound();
            }
            return View(prodotti);
        }

        // POST: Prodottis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Prezzo,QuantitaDisponibile")] Prodotti prodotti)
        {
            if (id != prodotti.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prodotti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdottiExists(prodotti.Id))
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
            return View(prodotti);
        }

        // GET: Prodottis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prodottis == null)
            {
                return NotFound();
            }

            var prodotti = await _context.Prodottis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prodotti == null)
            {
                return NotFound();
            }

            return View(prodotti);
        }

        // POST: Prodottis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prodottis == null)
            {
                return Problem("Entity set 'EcommerceContext.Prodottis'  is null.");
            }
            var prodotti = await _context.Prodottis.FindAsync(id);
            if (prodotti != null)
            {
                _context.Prodottis.Remove(prodotti);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdottiExists(int id)
        {
          return (_context.Prodottis?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
