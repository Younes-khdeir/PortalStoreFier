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
    public class NewPricePacksController : Controller
    {
        private readonly PortalContext _context;

        public NewPricePacksController(PortalContext context)
        {
            _context = context;
        }

        // GET: NewPricePacks
        public async Task<IActionResult> Index()
        {
            var portalContext = _context.NewPricePacks.Include(n => n.Customer).Include(n => n.PricePack);
            return View(await portalContext.ToListAsync());
        }

        // GET: NewPricePacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NewPricePacks == null)
            {
                return NotFound();
            }

            var newPricePack = await _context.NewPricePacks
                .Include(n => n.Customer)
                .Include(n => n.PricePack)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newPricePack == null)
            {
                return NotFound();
            }

            return View(newPricePack);
        }

        // GET: NewPricePacks/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name");
            ViewData["PricePackId"] = new SelectList(_context.PricePacks, "Id", "Service");
            return View();
        }

        // POST: NewPricePacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ContractStartingDate,ContractEndDate,CustomerId,PricePackId")] NewPricePack newPricePack)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newPricePack);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", newPricePack.CustomerId);
            ViewData["PricePackId"] = new SelectList(_context.PricePacks, "Id", "Service", newPricePack.PricePackId);
            return View(newPricePack);
        }

        // GET: NewPricePacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NewPricePacks == null)
            {
                return NotFound();
            }

            var newPricePack = await _context.NewPricePacks.FindAsync(id);
            if (newPricePack == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", newPricePack.CustomerId);
            ViewData["PricePackId"] = new SelectList(_context.PricePacks, "Id", "Service", newPricePack.PricePackId);
            return View(newPricePack);
        }

        // POST: NewPricePacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ContractStartingDate,ContractEndDate,CustomerId,PricePackId")] NewPricePack newPricePack)
        {
            if (id != newPricePack.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newPricePack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewPricePackExists(newPricePack.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", newPricePack.CustomerId);
            ViewData["PricePackId"] = new SelectList(_context.PricePacks, "Id", "Service", newPricePack.PricePackId);
            return View(newPricePack);
        }

        // GET: NewPricePacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NewPricePacks == null)
            {
                return NotFound();
            }

            var newPricePack = await _context.NewPricePacks
                .Include(n => n.Customer)
                .Include(n => n.PricePack)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newPricePack == null)
            {
                return NotFound();
            }

            return View(newPricePack);
        }

        // POST: NewPricePacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NewPricePacks == null)
            {
                return Problem("Entity set 'PortalContext.NewPricePacks'  is null.");
            }
            var newPricePack = await _context.NewPricePacks.FindAsync(id);
            if (newPricePack != null)
            {
                _context.NewPricePacks.Remove(newPricePack);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewPricePackExists(int id)
        {
          return (_context.NewPricePacks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
