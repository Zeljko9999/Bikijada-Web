using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bikijada_MVC.DAL;
using Bikijada_MVC.Models;
using System.Net;
using System.Web;

namespace Bikijada_MVC.Controllers
{
    public class VlasniciController : Controller
    {

        private BikijadaContext db = new BikijadaContext();

        // GET: Vlasnici
        public ActionResult Index()
        {
            return View(db.Vlasnik.ToList());
        }

        // GET: Vlasnici/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            } 
            var bik = db.Bik;
            ViewBag.ownedBulls = bik.Where(x => x.VlasnikId == id).ToList();

            var vlasnik = db.Vlasnik.Find(id);
            if (vlasnik == null)
            {
                return NotFound();
            }
            return View(vlasnik);
        }

        // GET: Vlasnici/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vlasnici/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ID,Ime,Mjesto")] Vlasnik vlasnik)
        {
            if (ModelState.IsValid)
            {
                db.Vlasnik.Add(vlasnik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vlasnik);
        }

        // GET: Vlasnici/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            var vlasnik = db.Vlasnik.Find(id);
            if (vlasnik == null)
            {
                return NotFound();
            }
            return View(vlasnik);
        }

        // POST: Vlasnici/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ID,Ime,Mjesto")] Vlasnik vlasnik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vlasnik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vlasnik);
        }


        // GET: Vlasnici/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vlasnik = db.Vlasnik.Find(id);
            if (vlasnik == null)
            {
                return NotFound();
            }
            return View(vlasnik);
        }

        // POST: Vlasnici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var vlasnik = db.Vlasnik.Find(id);
            db.Vlasnik.Remove(vlasnik);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}