using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortalStoreFier.Data;
using PortalStoreFier.Models;

namespace PortalStoreFier.Controllers
{
    public class PricePacksController : Controller
    {
        private readonly PortalContext _context;

        public PricePacksController(PortalContext context)
        {
            _context = context;
        }

        // GET: PricePacks
        public async Task<IActionResult> Index()
        {
              return _context.PricePacks != null ? 
                          View(await _context.PricePacks.ToListAsync()) :
                          Problem("Entity set 'PortalContext.PricePacks'  is null.");
        }

        // GET: PricePacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PricePacks == null)
            {
                return NotFound();
            }

            var pricePack = await _context.PricePacks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pricePack == null)
            {
                return NotFound();
            }

            return View(pricePack);
        }

        // GET: PricePacks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PricePacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Service,Details,Notes,Cost")] PricePack pricePack)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pricePack);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pricePack);
        }

        // GET: PricePacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PricePacks == null)
            {
                return NotFound();
            }

            var pricePack = await _context.PricePacks.FindAsync(id);
            if (pricePack == null)
            {
                return NotFound();
            }
            return View(pricePack);
        }

        // POST: PricePacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Service,Details,Notes,Cost")] PricePack pricePack)
        {
            if (id != pricePack.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pricePack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PricePackExists(pricePack.Id))
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
            return View(pricePack);
        }

        // GET: PricePacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PricePacks == null)
            {
                return NotFound();
            }

            var pricePack = await _context.PricePacks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pricePack == null)
            {
                return NotFound();
            }

            return View(pricePack);
        }

        // POST: PricePacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PricePacks == null)
            {
                return Problem("Entity set 'PortalContext.PricePacks'  is null.");
            }
            var pricePack = await _context.PricePacks.FindAsync(id);
            if (pricePack != null)
            {
                _context.PricePacks.Remove(pricePack);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PricePackExists(int id)
        {
          return (_context.PricePacks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
