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

        List<string> kategorijaList = new List<string> { "Poluteška", "Teška" };

        // GET: Oklade
        public ActionResult Index()
        {
            var oklade = db.Oklada.Include(r => r.Bik);
            return View(oklade.ToList());
        }

        // GET: Oklade/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var oklada = db.Oklada.Include(r => r.Bik).SingleOrDefault(x => x.ID == id);
            if (oklada == null)
            {
                return NotFound(); ;
            }
            return View(oklada);
        }

        // GET: Oklade/Create
        public ActionResult Create()
        {
            var vlasnici = db.Vlasnik;

            int? firstId = vlasnici.FirstOrDefault()?.ID;
            ViewBag.Vlasnik = new SelectList(vlasnici, "Ime", "Ime", firstId);
            ViewBag.BikId = new SelectList(db.Bik.Where(b => b.VlasnikId == firstId && b.Kategorija == "Poluteška"), "ID", "Ime");

            ViewBag.Kategorija = new SelectList(kategorijaList);
            return View();
        }
    


        // POST: Oklade/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ID,Ime,Iznos,Kategorija,Vlasnik,BikId")] Oklada oklada)
        {
            if (ModelState.IsValid)
            {
                db.Oklada.Add(oklada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            int firstId = db.Vlasnik.FirstOrDefault().ID;
            ViewBag.BikId = new SelectList(db.Bik.Where(b => b.VlasnikId == firstId && b.Kategorija == "Poluteška"), "ID", "Ime", oklada.BikId);
            ViewBag.Vlasnik = new SelectList(db.Vlasnik, "Ime", "Ime", firstId);

            ViewBag.Kategorija = new SelectList(kategorijaList);

            return View(oklada);
        }

        // GET: Oklade/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var oklada = db.Oklada.Find(id);
            if (oklada == null)
            {
                return NotFound();
            }
            int firstId = db.Vlasnik.FirstOrDefault().ID;
            ViewBag.Vlasnik = new SelectList(db.Vlasnik, "Ime", "Ime", firstId);
            ViewBag.BikId = new SelectList(db.Bik.Where(b => b.VlasnikId == firstId), "ID", "Ime", oklada.BikId);


            ViewBag.Kategorija = new SelectList(kategorijaList);
            return View(oklada);
        }

        // POST: Oklade/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ID,Ime,Iznos,Kategorija,Vlasnik,BikId")] Oklada oklada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oklada).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            int firstId = db.Vlasnik.FirstOrDefault().ID;

            ViewBag.BikId = new SelectList(db.Bik, "ID", "Ime", oklada.BikId);
            ViewBag.Vlasnik = new SelectList(db.Vlasnik, "Ime", "Ime", firstId);

            ViewBag.Kategorija = new SelectList(kategorijaList);
            return View(oklada);
        }


        // GET: Oklade/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var oklada = db.Oklada.Include(r => r.Bik).SingleOrDefault(x => x.ID == id);
            if (oklada == null)
            {
                return NotFound();
            }
            return View(oklada);
        }

        // POST: Oklade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var oklada = db.Oklada.Find(id);
            db.Oklada.Remove(oklada);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //Pretraga prema bikovima
        // GET: Oklade/More
        public ActionResult More()
        {
            var vlasnici = db.Vlasnik;

            int? firstId = vlasnici.FirstOrDefault()?.ID;
            ViewBag.Vlasnik = new SelectList(vlasnici, "Ime", "Ime", firstId);
            ViewBag.BikId = new SelectList(db.Bik.Where(b => b.VlasnikId == firstId && b.Kategorija == "Poluteška"), "ID", "Ime");

            ViewBag.Kategorija = new SelectList(kategorijaList);
            return View();
        }

        public ActionResult GetBetsForBull(string? vlasnikName, string? kategorija, int? bikId)
        {
            if (bikId != null)
            {
                var newBets = db.Oklada.Where(x => x.Vlasnik == vlasnikName && x.Kategorija.Equals(kategorija) && 
                x.BikId.Equals(bikId)).ToList();

                return Json(newBets);   
            }
            else
            {
                var error = "Bik not found";
                return Json(error);
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OkladaExists(int id)
        {
            return db.Oklada.Any(e => e.ID == id);
        }
    }
}

