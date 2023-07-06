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
    public class CustomersController : Controller
    {
        private readonly PortalContext _context;

        public CustomersController(PortalContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {

            var portalContext = _context.Customers.Include(c => c.Classification);
            return View(await portalContext.ToListAsync());

            //return _context.Customers != null ? 
            //              View(await _context.Customers.ToListAsync()) :
            //              Problem("Entity set 'PortalContext.Customers'  is null.");
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Classification)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["ClassificationId"] = new SelectList(_context.Classifications, "Id", "ClassificationName");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile? CompanyLogoFile, Customer customer)
        {
            if (ModelState.IsValid)
            {

                // Handle the image file
                if (CompanyLogoFile != null && CompanyLogoFile.Length > 0)
                {
                    // Generate a unique file name or use a different approach as per your requirements
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(CompanyLogoFile.FileName);

                    // Save the file to a directory
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await CompanyLogoFile.CopyToAsync(stream);
                    }

                    // Set the PostDesign property of the model to the saved file path or URL
                    customer.CompanyLogo = "/Images/" + fileName; // Assuming images are stored in the "images" folder in the wwwroot directory
                }


                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassificationId"] = new SelectList(_context.Classifications, "Id", "ClassificationName", customer.ClassificationId);

            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            ViewData["ClassificationId"] = new SelectList(_context.Classifications, "Id", "ClassificationName", customer.ClassificationId);

            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile? CompanyLogoFile, Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Handle the image file
                    if (CompanyLogoFile != null && CompanyLogoFile.Length > 0)
                    {
                        // Generate a unique file name or use a different approach as per your requirements
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(CompanyLogoFile.FileName);

                        // Save the file to a directory
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await CompanyLogoFile.CopyToAsync(stream);
                        }

                        // Set the PostDesign property of the model to the saved file path or URL
                        customer.CompanyLogo = "/Images/" + fileName; // Assuming images are stored in the "images" folder in the wwwroot directory
                    }

                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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

            ViewData["ClassificationId"] = new SelectList(_context.Classifications, "Id", "ClassificationName", customer.ClassificationId);

            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Classification)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'PortalContext.Customers'  is null.");
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
          return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
