using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompanyAirline.Models.Company;

namespace CompanyAirline.Controllers
{
    public class VolsController : Controller
    {
        private readonly CompanyDbContext _context;

        public VolsController(CompanyDbContext context)
        {
            _context = context;
        }

        // GET: Vols
        public async Task<IActionResult> Index()
        {
            var companyDbContext = _context.Vols.Include(v => v.Pilote);
            return View(await companyDbContext.ToListAsync());
        }
        public async Task<IActionResult> VolsAndTheirPilotes()
        {
            var CompanyDbContext = _context.Vols.Include(m => m.Pilote);
            return View(await CompanyDbContext.ToListAsync());
        }

        // GET: Vols/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vols == null)
            {
                return NotFound();
            }

            var vol = await _context.Vols
                .Include(v => v.Pilote)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vol == null)
            {
                return NotFound();
            }

            return View(vol);
        }
        public IActionResult SearchBy2()
        {
            
            var vols = _context.Vols.ToList();
            ViewBag.Avion = vols.Select(m => m.Avion).Distinct().OrderBy(g => g).ToList();
            return View(vols);

        }
        [HttpPost]
        public IActionResult SearchBy2(string avion, string ville_depart)
        {

            var vols = _context.Vols.ToList();
            ViewBag.Avion = vols.Select(m => m.Avion).ToList();
            ViewBag.Avion = avion;
            if (!string.IsNullOrEmpty(avion) && avion != "All")
            {
                vols = vols.Where(m => m.Avion == avion).ToList();
            }

            if (!string.IsNullOrEmpty(ville_depart))
            {
         
                vols = vols.Where(m => m.VilleDepart.Contains(ville_depart)).ToList();
            }
            if (avion == "ALL")
            {
                vols = _context.Vols.ToList();
            }
            if (avion == "ALL" && !string.IsNullOrEmpty(ville_depart))
            {
                vols = vols.Where(m => m.VilleDepart.Contains(ville_depart)).ToList();
            }
            return View("SearchBy2", vols);
        }

        // GET: Vols/Create
        public IActionResult Create()
        {
            ViewData["PiloteId"] = new SelectList(_context.Pilotes, "Id", "Id");
            return View();
        }

        // POST: Vols/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VilleDepart,VilleArrive,Tarif,Avion,PiloteId")] Vol vol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PiloteId"] = new SelectList(_context.Pilotes, "Id", "Id", vol.PiloteId);
            return View(vol);
        }

        // GET: Vols/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vols == null)
            {
                return NotFound();
            }

            var vol = await _context.Vols.FindAsync(id);
            if (vol == null)
            {
                return NotFound();
            }
            ViewData["PiloteId"] = new SelectList(_context.Pilotes, "Id", "Id", vol.PiloteId);
            return View(vol);
        }

        // POST: Vols/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VilleDepart,VilleArrive,Tarif,Avion,PiloteId")] Vol vol)
        {
            if (id != vol.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolExists(vol.Id))
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
            ViewData["PiloteId"] = new SelectList(_context.Pilotes, "Id", "Id", vol.PiloteId);
            return View(vol);
        }

        // GET: Vols/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vols == null)
            {
                return NotFound();
            }

            var vol = await _context.Vols
                .Include(v => v.Pilote)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vol == null)
            {
                return NotFound();
            }

            return View(vol);
        }

        // POST: Vols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vols == null)
            {
                return Problem("Entity set 'CompanyDbContext.Vols'  is null.");
            }
            var vol = await _context.Vols.FindAsync(id);
            if (vol != null)
            {
                _context.Vols.Remove(vol);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolExists(int id)
        {
          return (_context.Vols?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
