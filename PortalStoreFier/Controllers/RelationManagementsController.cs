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
    public class RelationManagementsController : Controller
    {
        private readonly PortalContext _context;

        public RelationManagementsController(PortalContext context)
        {
            _context = context;
        }

        // GET: RelationManagements
        public async Task<IActionResult> Index()
        {
            var portalContext = _context.RelationManagements.Include(r => r.Customer);
            return View(await portalContext.ToListAsync());
        }

        // GET: CustomerActivities
        public async Task<IActionResult> IndexCustomer(int? id)
        {
            IQueryable<RelationManagement> query = _context.RelationManagements;

            if (id.HasValue)
            {
                query = query.Where(c => c.CustomerId == id.Value);
            }

            query = query.Include(c => c.Customer);

            var RelationManagements = await query.ToListAsync();
            return View(RelationManagements);
        }

        // GET: RelationManagements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RelationManagements == null)
            {
                return NotFound();
            }

            var relationManagement = await _context.RelationManagements
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relationManagement == null)
            {
                return NotFound();
            }

            return View(relationManagement);
        }

        // GET: RelationManagements/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name");
            return View();
        }

        // POST: RelationManagements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Employee_Name,Result_type,Result,Notes,Date,CustomerId")] RelationManagement relationManagement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(relationManagement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", relationManagement.CustomerId);
            return View(relationManagement);
        }

        // GET: RelationManagements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RelationManagements == null)
            {
                return NotFound();
            }

            var relationManagement = await _context.RelationManagements.FindAsync(id);
            if (relationManagement == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", relationManagement.CustomerId);
            return View(relationManagement);
        }

        // POST: RelationManagements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Employee_Name,Result_type,Result,Notes,Date,CustomerId")] RelationManagement relationManagement)
        {
            if (id != relationManagement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(relationManagement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelationManagementExists(relationManagement.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", relationManagement.CustomerId);
            return View(relationManagement);
        }

        // GET: RelationManagements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RelationManagements == null)
            {
                return NotFound();
            }

            var relationManagement = await _context.RelationManagements
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relationManagement == null)
            {
                return NotFound();
            }

            return View(relationManagement);
        }

        // POST: RelationManagements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RelationManagements == null)
            {
                return Problem("Entity set 'PortalContext.RelationManagements'  is null.");
            }
            var relationManagement = await _context.RelationManagements.FindAsync(id);
            if (relationManagement != null)
            {
                _context.RelationManagements.Remove(relationManagement);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RelationManagementExists(int id)
        {
          return (_context.RelationManagements?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
