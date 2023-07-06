using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic.FileIO;
using PortalStoreFier.Data;
using PortalStoreFier.Models;
using System.Linq;


namespace PortalStoreFier.Controllers
{
    public class CustomerActivitiesController : Controller
    {
        private readonly PortalContext _context;

        public CustomerActivitiesController(PortalContext context)
        {
            _context = context;
        }

        // GET: CustomerActivities
        public async Task<IActionResult> Index()
        {
            var portalContext = _context.CustomerActivitys.Include(c => c.Customer);
            return View(await portalContext.ToListAsync());
        }

        // GET: CustomerActivities
        public async Task<IActionResult> IndexCustomer(int? id)
        {
            IQueryable<CustomerActivity> query = _context.CustomerActivitys;

            if (id.HasValue)
            {
                query = query.Where(c => c.CustomerId == id.Value);
            }

            query = query.Include(c => c.Customer);

            var customerActivities = await query.ToListAsync();
            return View(customerActivities);
        }


       
        // GET: CustomerActivities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CustomerActivitys == null)
            {
                return NotFound();
            }

            var customerActivity = await _context.CustomerActivitys
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerActivity == null)
            {
                return NotFound();
            }

            return View(customerActivity);
        }

        // GET: CustomerActivities/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name");
            return View();
        }


        // POST: CustomerActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(IFormFile? postDesignFile, CustomerActivity customerActivity)
        {

           
            if (ModelState.IsValid)
            {

                // Handle the image file
                if (postDesignFile != null && postDesignFile.Length > 0)
                {
                    // Generate a unique file name or use a different approach as per your requirements
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(postDesignFile.FileName);

                    // Save the file to a directory
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await postDesignFile.CopyToAsync(stream);
                    }

                    // Set the PostDesign property of the model to the saved file path or URL
                    customerActivity.PostDesign = "/Images/" + fileName; // Assuming images are stored in the "images" folder in the wwwroot directory
                }

                _context.Add(customerActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", customerActivity.CustomerId);
            return View(customerActivity);
        }

        // GET: CustomerActivities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CustomerActivitys == null)
            {
                return NotFound();
            }

            var customerActivity = await _context.CustomerActivitys.FindAsync(id);
            if (customerActivity == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", customerActivity.CustomerId);
            return View(customerActivity);
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, IFormFile? postDesignFile, CustomerActivity customerActivity)
        //{
        //    if (id != customerActivity.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            // Retrieve the existing entity from the database
        //            var existingActivity = await _context.FindAsync<CustomerActivity>(id);

        //            if (existingActivity == null)
        //            {
        //                return NotFound();
        //            }

        //            // Detach the existing entity from the context
        //            _context.Entry(existingActivity).State = EntityState.Detached;

        //            // Delete the previous image if it exists
        //            if (!string.IsNullOrEmpty(existingActivity.PostDesign))
        //            {
        //                string previousImagePath = Path.Combine(Directory.GetCurrentDirectory(), existingActivity.PostDesign.TrimStart('/'));

        //                if (System.IO.File.Exists(previousImagePath))
        //                {
        //                    System.IO.File.Delete(previousImagePath);
        //                }
        //            }

        //            // Handle the new image file
        //            if (postDesignFile != null && postDesignFile.Length > 0)
        //            {
        //                // Process the uploaded image file and save it to your desired storage location
        //                var uniqueFileName = Guid.NewGuid().ToString() + "_" + postDesignFile.FileName;
        //                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", uniqueFileName);
        //                using (var stream = new FileStream(imagePath, FileMode.Create))
        //                {
        //                    await postDesignFile.CopyToAsync(stream);
        //                }

        //                // Update the PostDesign property with the saved image path
        //                customerActivity.PostDesign = "/Images/" + uniqueFileName;
        //            }

        //            // Update other properties as needed
        //            existingActivity.CustomerId = customerActivity.CustomerId;
        //            existingActivity.Employee_Name = customerActivity.Employee_Name;
        //            existingActivity.PostType = customerActivity.PostType;
        //            existingActivity.DatePostPublished = customerActivity.DatePostPublished;
        //            existingActivity.PostIdea = customerActivity.PostIdea;
        //            existingActivity.PostContent = customerActivity.PostContent;

        //            // Attach the updated entity to the context and mark it as modified
        //            _ = _context.FindAsync<CustomerActivity>(existingActivity);
        //            _context.Entry(existingActivity).State = EntityState.Modified;

        //            // Save the changes to the database
        //            await _context.SaveChangesAsync();

        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            // Handle concurrency issues if necessary
        //            throw;
        //        }
        //    }

        //    // If the model state is invalid, return to the Edit view with the model to display validation errors
        //    ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", customerActivity.CustomerId);
        //    return View(customerActivity);
        //}


        //// POST: CustomerActivities/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile? postDesignFile, CustomerActivity customerActivity)
        {
            if (id != customerActivity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing entity from the database
                    //var existingActivity = await _context.FindAsync<CustomerActivity>(id);

                    //if (existingActivity == null)
                    //{
                    //    return NotFound();
                    //}

                    //// Delete the previous image if it exists
                    //if (!string.IsNullOrEmpty(existingActivity.PostDesign))
                    //{
                    //    //string previousImagePath = Path.Combine(Directory.GetCurrentDirectory(), existingActivity.PostDesign.TrimStart('/'));
                    //    string previousImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingActivity.PostDesign);

                    //    if (System.IO.File.Exists(previousImagePath))
                    //    {
                    //        System.IO.File.Delete(previousImagePath);
                    //    }
                    //}

                    // Handle the image file
                    if (postDesignFile != null && postDesignFile.Length > 0)
                    {
                        // Generate a unique file name or use a different approach as per your requirements
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(postDesignFile.FileName);

                        // Save the file to a directory
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await postDesignFile.CopyToAsync(stream);
                        }

                        // Set the PostDesign property of the model to the saved file path or URL
                        customerActivity.PostDesign = "/Images/" + fileName; // Assuming images are stored in the "images" folder in the wwwroot directory
                    }
                    _context.Update(customerActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerActivityExists(customerActivity.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", customerActivity.CustomerId);
            return View(customerActivity);
        }

        // GET: CustomerActivities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CustomerActivitys == null)
            {
                return NotFound();
            }

            var customerActivity = await _context.CustomerActivitys
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerActivity == null)
            {
                return NotFound();
            }

            return View(customerActivity);
        }

        // POST: CustomerActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CustomerActivitys == null)
            {
                return Problem("Entity set 'PortalContext.CustomerActivitys'  is null.");
            }
            var customerActivity = await _context.CustomerActivitys.FindAsync(id);
            if (customerActivity != null)
            {
                _context.CustomerActivitys.Remove(customerActivity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerActivityExists(int id)
        {
          return (_context.CustomerActivitys?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
