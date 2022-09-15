using System;
using System.Collections.Generic;
//using System.Data.Entity;
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
    public class BreedsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BreedsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Breeds
        public async Task<IActionResult> Index()
        {
            

            return _context.Breed != null ?
                        View(await _context.Breed.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Breed'  is null.");
        }
        public async Task<IActionResult> SearchForm()
        {
            return _context.Breed != null ?
                        View(await _context.Breed.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Breed'  is null.");
        }
        public async Task<IActionResult> SearchResults(string SearchPhrase, SizeEnum SearchSize)
        {
            var SplitedSearchPhrase = SearchPhrase.Split(",");
            var Items = _context.Breed.ToList();
            var ItemsSelected  = new List<Breed>();
            foreach (var Item in Items)
            {
                var ItemSplited = Item.Description.Split(",");
                if (SplitedSearchPhrase.All(elem => ItemSplited.Contains(elem)))
                {
                    ItemsSelected.Add(Item);
                }
            }
            var Result = ItemsSelected.Where(j => j.Size.Equals(SearchSize));
            return _context.Breed != null ?
                        View("Index", Result) :
                        Problem("Entity set 'ApplicationDbContext.Breed'  is null.");
        }



        // GET: Breeds/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Breed == null)
            {
                return NotFound();
            }

            var breed = await _context.Breed
                .FirstOrDefaultAsync(m => m.id == id);
            if (breed == null)
            {
                return NotFound();
            }

            return View(breed);
        }

        // GET: Breeds/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            var FeatureList = (from f in _context.Feature
                               select new SelectListItem()
                               {
                                   Text = f.Name,
                                   Value = f.Id.ToString()
                               }).ToList();
            FeatureList.Insert(0, new SelectListItem()
            {
                Text = "--Select--",
                Value = "select"
            });
            ViewBag.FeatureList = FeatureList;
            return View();
        }

        // POST: Breeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Size,Description,Favourite,Features")] Breed breed)
        {
            if (ModelState.IsValid)
            {
                breed.id = Guid.NewGuid();
                _context.Add(breed);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(breed);
        }

        // GET: Breeds/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Breed == null)
            {
                return NotFound();
            }

            var breed = await _context.Breed.FindAsync(id);
            if (breed == null)
            {
                return NotFound();
            }
            return View(breed);
        }

        // POST: Breeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,Name,Size,Description,Favourite,Features")] Breed breed)
        {
            if (id != breed.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(breed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BreedExists(breed.id))
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
            return View(breed);
        }

        // GET: Breeds/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Breed == null)
            {
                return NotFound();
            }

            var breed = await _context.Breed
                .FirstOrDefaultAsync(m => m.id == id);
            if (breed == null)
            {
                return NotFound();
            }

            return View(breed);
        }

        // POST: Breeds/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Breed == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Breed'  is null.");
            }
            var breed = await _context.Breed.FindAsync(id);
            if (breed != null)
            {
                _context.Breed.Remove(breed);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BreedExists(Guid id)
        {
          return (_context.Breed?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
