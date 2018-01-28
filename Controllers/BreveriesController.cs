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
    public class BreveriesController : Controller
    {
        private readonly PolskieBrowaryContext _context;

        public BreveriesController(PolskieBrowaryContext context)
        {
            _context = context;
        }

        // GET: Breveries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Brevery.OrderBy(m => m.BreveryName).ToListAsync());
        }

        // GET: Breveries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brevery = await _context.Brevery
                .SingleOrDefaultAsync(m => m.BreveryID == id);
            if (brevery == null)
            {
                return NotFound();
            }

            return View(brevery);
        }

        // GET: Breveries/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Breveries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BreveryID,BreveryName,City,Description,Established,ProductionCapacity")] Brevery brevery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brevery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brevery);
        }

        // GET: Breveries/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brevery = await _context.Brevery.SingleOrDefaultAsync(m => m.BreveryID == id);
            if (brevery == null)
            {
                return NotFound();
            }
            return View(brevery);
        }

        // POST: Breveries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BreveryID,BreveryName,City,Description,Established,ProductionCapacity")] Brevery brevery)
        {
            if (id != brevery.BreveryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brevery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BreveryExists(brevery.BreveryID))
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
            return View(brevery);
        }

        // GET: Breveries/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brevery = await _context.Brevery
                .SingleOrDefaultAsync(m => m.BreveryID == id);
            if (brevery == null)
            {
                return NotFound();
            }

            return View(brevery);
        }

        // POST: Breveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brevery = await _context.Brevery.SingleOrDefaultAsync(m => m.BreveryID == id);
            _context.Brevery.Remove(brevery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BreveryExists(int id)
        {
            return _context.Brevery.Any(e => e.BreveryID == id);
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> VerifyBreveryName(string BreveryName)
        {
            var brevery = await _context.Brevery.FirstOrDefaultAsync(e => e.BreveryName == BreveryName);

            if (brevery == null)
                return Json(true);
            else
                return Json($"Podana nazwa browaru juz istnieje");
        }
    }
}
