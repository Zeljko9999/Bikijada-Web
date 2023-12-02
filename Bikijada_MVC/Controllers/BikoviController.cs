using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bikijada_MVC.DAL;
using System.Net;

namespace Bikijada_MVC.Controllers
{
    public class BikoviController : Controller
    {
        private BikijadaContext db = new BikijadaContext();

        List<string> kategorijaList = new List<string> { "Poluteška", "Teška" };

        // GET: Bikovi
        public ActionResult Index()
        {
            var bikovi = db.Bik.Include(v => v.Vlasnik);
            return View(bikovi.ToList());
        }

        // GET: Bikovi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

        //****  OWNED FIGHTS ****

            var borba = db.Borba;
            ViewBag.ownedFights = borba.Where(x => x.Bik1Id == id || x.Bik2Id == id).ToList();
            List<string> protivnici = new List<string> { };

            foreach (var fight in ViewBag.ownedFights)
            {
                
                if (fight.Bik2Id != id) {     
                    int fID = fight.Bik2Id;
                    var protivnik = db.Bik;
                    ViewBag.bull = protivnik.Where(x => x.ID == fID).ToList();
                    foreach (var bull in ViewBag.bull)
                    {
                        string opponent = (string) bull.Ime;
                        protivnici.Add(opponent);
                    }
                }
                else
                {
                    int fID = fight.Bik1Id;
                    var protivnik = db.Bik;
                    ViewBag.bull = protivnik.Where(x => x.ID == fID).ToList();
                    foreach (var bull in ViewBag.bull)
                    {
                        string opponent = (string)bull.Ime;
                        protivnici.Add(opponent);
                    }
                }
            }
            ViewBag.bulli = protivnici;
            var bik = db.Bik.Include(v => v.Vlasnik).SingleOrDefault(x => x.ID == id);
            if (bik == null)
            {
                return NotFound();
            }
            return View(bik);
        }

        // GET: Bikovi/Create
        public ActionResult Create(int? vlasnikId)
        {
            if (vlasnikId != null)
            {
                var vlasnik = db.Vlasnik.Find(vlasnikId);
                ViewBag.VlasnikId = new SelectList(db.Vlasnik, "ID", "Ime",vlasnik.ID);

            } else {

                ViewBag.VlasnikId = new SelectList(db.Vlasnik, "ID", "Ime");
            }

            ViewBag.Kategorija = new SelectList(kategorijaList);


            return View();
        }

        // POST: Bikovi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ID,Kategorija,VlasnikId,Ime")] Bik bik)
        {
            if (ModelState.IsValid)
            {
                db.Bik.Add(bik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VlasnikId = new SelectList(db.Vlasnik, "ID", "Ime", bik.VlasnikId);

            ViewBag.Kategorija = new SelectList(kategorijaList);

            return View(bik);
        }

        // GET: Bikovi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bik = db.Bik.Find(id);
            if (bik == null)
            {
                return NotFound();
            }
            ViewBag.VlasnikId = new SelectList(db.Vlasnik, "ID", "Ime", bik.VlasnikId);

            ViewBag.Kategorija = new SelectList(kategorijaList);

            return View(bik);
        }

        // POST: Bikovi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ID,Kategorija,VlasnikId,Ime")] Bik bik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VlasnikId = new SelectList(db.Vlasnik, "ID", "Ime", bik.VlasnikId);

            ViewBag.Kategorija = new SelectList(kategorijaList);

            return View(bik);
        }


        // GET: Bikovi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bik = db.Bik.Include(v => v.Vlasnik).SingleOrDefault(x => x.ID == id);
            if (bik == null)
            {
                return NotFound();
            }
            return View(bik);
        }

        // POST: Bikovi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bik bik = db.Bik.Find(id);
            db.Bik.Remove(bik);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetBullsForOwner(string? vlasnikName, string? kategorija)
        {
            if (vlasnikName != null)
            {
                int vlasnikId = db.Vlasnik
                  .Where(x => x.Ime == vlasnikName)
                  .Select(x => x.ID)
                  .FirstOrDefault();

                var newBulls = db.Bik.Where(x => x.VlasnikId == vlasnikId && x.Kategorija == kategorija).ToList();
                return Json(newBulls);      
            }
            else
            {
                var error = "vlasnikId not found";
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
        private bool BikExists(int id)
        {
          return db.Bik.Any(e => e.ID == id);
        }
    }
}