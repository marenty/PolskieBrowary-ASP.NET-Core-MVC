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
    public class CommentsController : Controller
    {
        private readonly PolskieBrowaryContext _context;

        public CommentsController(PolskieBrowaryContext context)
        {
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var polskieBrowaryContext = _context.Comment.Include(c => c.CommentedBeer);
            return View(await polskieBrowaryContext.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .Include(c => c.CommentedBeer)
                .SingleOrDefaultAsync(m => m.CommentID == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            ViewData["BeerId"] = new SelectList(_context.Beer, "BeerID", "BreveryName");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "User")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Comment_text,CommentedBeerId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.UserName = User.Identity.Name;
                comment.CommentData = DateTime.Now;
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Beers", new { @id = comment.CommentedBeerId });
            }
            return RedirectToAction("Details", "Beers", new { @id = comment.CommentedBeerId });
        }

        // GET: Comments/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment.SingleOrDefaultAsync(m => m.CommentID == id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["BeerId"] = new SelectList(_context.Beer, "BeerID", "BreveryName", comment.CommentedBeerId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentID,Comment_text,CommentData,UserName,CommentedBeerId")] Comment comment)
        {
            if (id != comment.CommentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.CommentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Beers", new { @id = comment.CommentedBeerId });
            }
            ViewData["BeerId"] = new SelectList(_context.Beer, "BeerID", "BreveryName", comment.CommentedBeerId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .Include(c => c.CommentedBeer)
                .SingleOrDefaultAsync(m => m.CommentID == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comment.SingleOrDefaultAsync(m => m.CommentID == id);
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Beers", new { @id = comment.CommentedBeerId });
        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.CommentID == id);
        }
    }
}
