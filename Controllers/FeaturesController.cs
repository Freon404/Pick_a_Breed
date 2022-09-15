using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pick_a_Breed.Data;
using Pick_a_Breed.Models;

namespace Pick_a_Breed.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class FeaturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeaturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Features
        public async Task<IActionResult> Index()
        {
              return _context.Feature != null ? 
                          View(await _context.Feature.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Feature'  is null.");
        }

        // GET: Features/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Feature == null)
            {
                return NotFound();
            }

            var feature = await _context.Feature
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feature == null)
            {
                return NotFound();
            }

            return View(feature);
        }

        // GET: Features/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Features/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Feature feature)
        {
            if (ModelState.IsValid)
            {
                feature.Id = Guid.NewGuid();
                _context.Add(feature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(feature);
        }

        // GET: Features/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Feature == null)
            {
                return NotFound();
            }

            var feature = await _context.Feature.FindAsync(id);
            if (feature == null)
            {
                return NotFound();
            }
            return View(feature);
        }

        // POST: Features/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] Feature feature)
        {
            if (id != feature.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeatureExists(feature.Id))
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
            return View(feature);
        }

        // GET: Features/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Feature == null)
            {
                return NotFound();
            }

            var feature = await _context.Feature
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feature == null)
            {
                return NotFound();
            }

            return View(feature);
        }

        // POST: Features/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Feature == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Feature'  is null.");
            }
            var feature = await _context.Feature.FindAsync(id);
            if (feature != null)
            {
                _context.Feature.Remove(feature);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeatureExists(Guid id)
        {
          return (_context.Feature?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
