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
    public class BeersController : Controller
    {
        private readonly PolskieBrowaryContext _context;

        public BeersController(PolskieBrowaryContext context)
        {
            _context = context;
        }

        // GET: Beers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Beer.Include(m => m.Brevery).OrderBy(m => m.BreveryName).Include(m => m.BeerGenre).ToListAsync());
        }

        // GET: Beers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beer
                .Include(m => m.Brevery).Include(m => m.BeerGenre).SingleOrDefaultAsync(m => m.BeerID == id);
            var comments = await _context.Comment.Where(m => m.CommentedBeerId == id).OrderByDescending(a => a.CommentData).ToListAsync();
            ViewData["comments"] = comments;

            Comment com = new Comment();
            com.CommentedBeerId = beer.BeerID;
            ViewData["comment-form"] = com;

            if (beer == null)
            {
                return NotFound();
            }

            return View(beer);
        }

        // GET: Beers/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["BeerGenreID"] = new SelectList(_context.BeerGenre, "BeerGenreID", "GenreName");
            ViewData["BreveryID"] = new SelectList(_context.Brevery, "BreveryID", "BreveryName");
            return View();
        }

        // POST: Beers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BeerID,BreveryName,AlcoholContent,ExtractContent,Price,BreveryID,BeerGenreID")] Beer beer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BeerGenreID"] = new SelectList(_context.BeerGenre, "BeerGenreID", "GenreName", beer.BeerGenreID);
            ViewData["BreveryID"] = new SelectList(_context.Brevery, "BreveryID", "BreveryName", beer.BreveryID);
            return View(beer);
        }

        // GET: Beers/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beer.Include(m => m.Brevery).Include(m => m.BeerGenre).SingleOrDefaultAsync(m => m.BeerID == id);
            if (beer == null)
            {
                return NotFound();
            }

            ViewData["BeerGenreID"] = new SelectList(_context.BeerGenre, "BeerGenreID", "GenreName", beer.BeerGenreID);
            ViewData["BreveryID"] = new SelectList(_context.Brevery, "BreveryID", "BreveryName", beer.BreveryID);
            return View(beer);
        }

        // POST: Beers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BeerID,BreveryName,AlcoholContent,ExtractContent,Price,BreveryID,BeerGenreID")] Beer beer)
        {
            if (id != beer.BeerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeerExists(beer.BeerID))
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
            return View(beer);
        }

        // GET: Beers/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beer
                .Include(m => m.Brevery).Include(m => m.BeerGenre).SingleOrDefaultAsync(m => m.BeerID == id);
            if (beer == null)
            {
                return NotFound();
            }

            return View(beer);
        }

        // POST: Beers/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var beer = await _context.Beer.SingleOrDefaultAsync(m => m.BeerID == id);
            _context.Beer.Remove(beer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeerExists(int id)
        {
            return _context.Beer.Any(e => e.BeerID == id);
        }


        public async Task<IActionResult> BeersInGenre(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beer.Include(m => m.Brevery).Include(m => m.BeerGenre).OrderBy(m => m.BreveryName).Where(m => m.BeerGenreID == id).ToListAsync();

            if (beer == null)
            {
                return NotFound();
            }

            return View("Index", beer);
        }

        public async Task<IActionResult> BeersInBrevery(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beer.Include(m => m.Brevery).Include(m => m.BeerGenre).OrderBy(m => m.BreveryName).Where(m => m.BreveryID == id).ToListAsync();

            if (beer == null)
            {
                return NotFound();
            }

            return View("Index", beer);
        }
    }
}

