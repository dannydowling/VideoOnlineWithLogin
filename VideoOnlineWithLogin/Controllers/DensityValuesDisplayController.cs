using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PreFlight.DataContexts;
using PreFlight.Types;

namespace PreFlight.Controllers
{
    public class DensityValuesDisplayController : Controller
    {
        private DensityContext db = new DensityContext();

        // GET: DensityValuesDisplay
        public async Task<ActionResult> Index()
        {
            var locations = db.Locations.Include(l => l.Weather);
            return View(await locations.ToListAsync());
        }

        // GET: DensityValuesDisplay/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = await db.Locations.FindAsync(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: DensityValuesDisplay/Create
        public ActionResult Create()
        {
            ViewBag.WeatherId = new SelectList(db.WeatherTable, "Id", "Id");
            return View();
        }

        // POST: DensityValuesDisplay/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Icao,Airport,City,State,Latitude,Longitude,WeatherId,RowVersion")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Locations.Add(location);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.WeatherId = new SelectList(db.WeatherTable, "Id", "Id", location.WeatherId);
            return View(location);
        }

        // GET: DensityValuesDisplay/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = await db.Locations.FindAsync(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            ViewBag.WeatherId = new SelectList(db.WeatherTable, "Id", "Id", location.WeatherId);
            return View(location);
        }

        // POST: DensityValuesDisplay/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Icao,Airport,City,State,Latitude,Longitude,WeatherId,RowVersion")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.WeatherId = new SelectList(db.WeatherTable, "Id", "Id", location.WeatherId);
            return View(location);
        }

        // GET: DensityValuesDisplay/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = await db.Locations.FindAsync(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: DensityValuesDisplay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Location location = await db.Locations.FindAsync(id);
            db.Locations.Remove(location);
            await db.SaveChangesAsync();
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
