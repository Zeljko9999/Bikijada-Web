using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bikijada_MVC.DAL;
using Bikijada_MVC.Models;

namespace Bikijada_MVC.Controllers
{
    public class OkladeController : Controller
    {
        private BikijadaContext db = new BikijadaContext();

        // GET: Oklade
        public ActionResult Index()
        {
            var oklade = db.Oklada.Include(r => r.Borba);
            return View(oklade.ToList());
        }

        // GET: Oklade/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var oklada = db.Oklada.Include(r => r.Borba).SingleOrDefault(x => x.ID == id);
            if (oklada == null)
            {
                return NotFound(); ;
            }
            return View(oklada);
        }

        // GET: Oklade/Create
        public ActionResult Create()
        {
            ViewBag.Borba = new SelectList(db.Borba, "ID", "ID");
            return View();
        }
    }
}

        // POST: Oklade/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
/*       [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ID,Ime,Iznos,Bik,Vlasnik")] Oklada oklada)
        {
            if (ModelState.IsValid)
            {
                db.Oklada.Add(oklada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Bik1Id = new SelectList(db.Bik, "ID", "Ime", borba.Bik1Id);
            ViewBag.Vlasnik1 = new SelectList(db.Bik, "Ime", "Ime", borba.Vlasnik1);
            ViewBag.Bik2Id = new SelectList(db.Bik, "ID", "Ime", borba.Bik2Id);
            ViewBag.Vlasnik2 = new SelectList(db.Bik, "Ime", "Ime", borba.Vlasnik2);
            return View(borba);
        }
        public async Task<IActionResult> Create([Bind("ID,Ime,Iznos,Bik,Vlasnik")] Oklada oklada)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oklada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(oklada);
        }

        // GET: Oklade/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Oklada == null)
            {
                return NotFound();
            }

            var oklada = await _context.Oklada.FindAsync(id);
            if (oklada == null)
            {
                return NotFound();
            }
            return View(oklada);
        }

        // POST: Oklade/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Ime,Iznos,Bik,Vlasnik")] Oklada oklada)
        {
            if (id != oklada.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oklada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OkladaExists(oklada.ID))
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
            return View(oklada);
        }

        // GET: Oklade/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Oklada == null)
            {
                return NotFound();
            }

            var oklada = await _context.Oklada
                .FirstOrDefaultAsync(m => m.ID == id);
            if (oklada == null)
            {
                return NotFound();
            }

            return View(oklada);
        }

        // POST: Oklade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Oklada == null)
            {
                return Problem("Entity set 'BikijadaContext.Oklada'  is null.");
            }
            var oklada = await _context.Oklada.FindAsync(id);
            if (oklada != null)
            {
                _context.Oklada.Remove(oklada);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OkladaExists(int id)
        {
          return _context.Oklada.Any(e => e.ID == id);
        }
    }
}
*/