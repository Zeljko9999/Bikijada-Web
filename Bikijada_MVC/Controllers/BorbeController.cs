﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bikijada_MVC.DAL;
using Bikijada_MVC.Models;
using System.Net;

namespace Bikijada_MVC.Controllers
{
    public class BorbeController : Controller
    {
        private BikijadaContext db = new BikijadaContext();

        List<string> kategorijaList = new List<string> { "Poluteška", "Teška" };

        // GET: Borbe
        public ActionResult Index()
        {
            var borbe = db.Borba.Include(r => r.Bik1).Include(r => r.Bik2);
            return View(borbe.ToList());
        }

        // GET: Borbe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var borba = db.Borba.Include(r => r.Bik1).Include(r => r.Bik2).SingleOrDefault(x => x.ID == id);
            if (borba == null)
            {
                return NotFound();
            }
            return View(borba);
        }

        // GET: Borbe/Create
        public ActionResult Create()
        {
            var vlasnici = db.Vlasnik;

            int? firstId = vlasnici.FirstOrDefault()?.ID;
            ViewBag.Vlasnik1 = new SelectList(vlasnici, "Ime", "Ime", firstId);
            ViewBag.Bik1Id = new SelectList(db.Bik.Where(b => b.VlasnikId == firstId && b.Kategorija == "Poluteška"), "ID", "Ime");
            ViewBag.Vlasnik2 = new SelectList(vlasnici, "Ime", "Ime", firstId);
            ViewBag.Bik2Id = new SelectList(db.Bik.Where(b => b.VlasnikId.Equals(firstId) && b.Kategorija == "Poluteška"), "ID", "Ime");

            ViewBag.Kategorija = new SelectList(kategorijaList);

            return View();
        }

        // POST: Borbe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ID,Date,Kategorija,Vlasnik1,Bik1Id,Vlasnik2,Bik2Id")] Borba borba)
        {
            if (ModelState.IsValid && borba.Bik1Id != borba.Bik2Id)
            {
                db.Borba.Add(borba);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            int firstId = db.Vlasnik.FirstOrDefault().ID;
            ViewBag.Vlasnik1 = new SelectList(db.Vlasnik, "Ime", "Ime", borba.Vlasnik1);
            ViewBag.Bik1Id = new SelectList(db.Bik.Where(b => b.VlasnikId == firstId && b.Kategorija == "Poluteška"), "ID", "Ime", borba.Bik1Id);
            ViewBag.Vlasnik2 = new SelectList(db.Vlasnik, "Ime", "Ime", borba.Vlasnik1);
            ViewBag.Bik2Id = new SelectList(db.Bik.Where(b => b.VlasnikId == firstId && b.Kategorija == "Poluteška"), "ID", "Ime", borba.Bik2Id);

            ViewBag.Kategorija = new SelectList(kategorijaList);

            return View(borba);
        }

        // GET: Borbe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var borba = db.Borba.Find(id);
            if (borba == null)
            {
                return NotFound();
            }

            int vlasnik1Idd = 0;
            ViewBag.newOwner = db.Vlasnik.Where(x => x.Ime == borba.Vlasnik1).ToList();
            foreach (var owner in ViewBag.newOwner)
            {
                vlasnik1Idd = owner.ID;
            }

            int vlasnik2Idd = 0;
            ViewBag.newOwner = db.Vlasnik.Where(x => x.Ime == borba.Vlasnik2).ToList();
            foreach (var owner in ViewBag.newOwner)
            {
                vlasnik2Idd = owner.ID;
            }


            ViewBag.Vlasnik1 = new SelectList(db.Vlasnik, "Ime", "Ime", borba.Vlasnik1);
            ViewBag.Bik1Id = new SelectList(db.Bik.Where(b => b.VlasnikId == vlasnik1Idd && b.Kategorija== borba.Kategorija), "ID", "Ime", borba.Bik1Id);
            ViewBag.Vlasnik2 = new SelectList(db.Vlasnik, "Ime", "Ime", borba.Vlasnik2);
            ViewBag.Bik2Id = new SelectList(db.Bik.Where(b => b.VlasnikId == vlasnik2Idd && b.Kategorija == borba.Kategorija), "ID", "Ime", borba.Bik2Id);

            ViewBag.Kategorija = new SelectList(kategorijaList,borba.Kategorija);
            return View(borba);
        }

        // POST: Borbe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ID,Date,Kategorija,Vlasnik1,Bik1Id,Vlasnik2,Bik2Id")] Borba borba)
        {
            if (ModelState.IsValid && borba.Bik1Id != borba.Bik2Id)
            {
                db.Entry(borba).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            int vlasnik1Idd = 0;
            ViewBag.newOwner = db.Vlasnik.Where(x => x.Ime == borba.Vlasnik1).ToList();
            foreach (var owner in ViewBag.newOwner)
            {
                vlasnik1Idd = owner.ID;
            }

            int vlasnik2Idd = 0;
            ViewBag.newOwner = db.Vlasnik.Where(x => x.Ime == borba.Vlasnik2).ToList();
            foreach (var owner in ViewBag.newOwner)
            {
                vlasnik2Idd = owner.ID;
            }


            ViewBag.Vlasnik1 = new SelectList(db.Vlasnik, "Ime", "Ime", borba.Vlasnik1);
            ViewBag.Bik1Id = new SelectList(db.Bik.Where(b => b.VlasnikId == vlasnik1Idd && b.Kategorija == borba.Kategorija), "ID", "Ime", borba.Bik1Id);
            ViewBag.Vlasnik2 = new SelectList(db.Vlasnik, "Ime", "Ime", borba.Vlasnik2);
            ViewBag.Bik2Id = new SelectList(db.Bik.Where(b => b.VlasnikId == vlasnik2Idd && b.Kategorija == borba.Kategorija), "ID", "Ime", borba.Bik2Id);

            ViewBag.Kategorija = new SelectList(kategorijaList, borba.Kategorija);
            return View(borba);
        }

        // GET: Borbe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var borba = db.Borba.Include(r => r.Bik1).Include(r => r.Bik2).SingleOrDefault(x => x.ID == id);
            if (borba == null)
            {
                return NotFound();
            }
            return View(borba);
        }

        // POST: Borbe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var borba = db.Borba.Find(id);
            db.Borba.Remove(borba);
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

        private bool BorbaExists(int id)
        {
          return db.Borba.Any(e => e.ID == id);
        }
    }
}