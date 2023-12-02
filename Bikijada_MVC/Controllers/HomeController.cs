using Bikijada_MVC.Models;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bikijada_MVC.DAL;
using System.Net;

namespace Bikijada_MVC.Controllers
{
    public class HomeController : Controller
    {
        private BikijadaContext db = new BikijadaContext();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: Home
        public IActionResult Index()
        {
            return View();
        }


        // POST: Home

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind("ID,Email,Naslov,Poruka")] Home msg)
        {
          
                if (ModelState.IsValid)
                {
                    db.Home.Add(msg);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(msg);

        }

        // GET: Home/Details
        public ActionResult Details()
        {
            return View(db.Home.ToList());
        }

        // POST: Home/Delete

        public ActionResult Delete(int id)
        {
            var msg = db.Home.Find(id);
            db.Home.Remove(msg);
            db.SaveChanges();
            return RedirectToAction("Details");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}