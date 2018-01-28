using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PolskieBrowary.Data;
using PolskieBrowary.Models;

namespace PolskieBrowary.Controllers
{
    public class BeerGenresController : Controller
    {
        private readonly PolskieBrowaryContext _context;

        public BeerGenresController(PolskieBrowaryContext context)
        {
            _context = context;
        }

        // GET: BeerGenres
        public async Task<IActionResult> Index()
        {
            return View(await _context.BeerGenre.OrderBy(m => m.GenreName).ToListAsync());
        }

        // GET: BeerGenres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beerGenre = await _context.BeerGenre
                .SingleOrDefaultAsync(m => m.BeerGenreID == id);
            if (beerGenre == null)
            {
                return NotFound();
            }

            return View(beerGenre);
        }

        // GET: BeerGenres/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: BeerGenres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BeerGenreID,GenreName,GenreDescription")] BeerGenre beerGenre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beerGenre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(beerGenre);
        }

        // GET: BeerGenres/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beerGenre = await _context.BeerGenre.SingleOrDefaultAsync(m => m.BeerGenreID == id);
            if (beerGenre == null)
            {
                return NotFound();
            }
            return View(beerGenre);
        }

        // POST: BeerGenres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("BeerGenreID,GenreName,GenreDescription")] BeerGenre beerGenre)
        {
            if (id != beerGenre.BeerGenreID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beerGenre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeerGenreExists(beerGenre.BeerGenreID))
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
            return View(beerGenre);
        }

        // GET: BeerGenres/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beerGenre = await _context.BeerGenre
                .SingleOrDefaultAsync(m => m.BeerGenreID == id);
            if (beerGenre == null)
            {
                return NotFound();
            }

            return View(beerGenre);
        }

        // POST: BeerGenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var beerGenre = await _context.BeerGenre.SingleOrDefaultAsync(m => m.BeerGenreID == id);
            _context.BeerGenre.Remove(beerGenre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeerGenreExists(int id)
        {
            return _context.BeerGenre.Any(e => e.BeerGenreID == id);
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> VerifyBeerGenre(string GenreName)
        {
            var genre = await _context.BeerGenre.FirstOrDefaultAsync(e => e.GenreName == GenreName);

            if (genre == null)
                return Json(true);
            else
                return Json($"Podana nazwa gatunku juz istnieje");
        }
    }
}
