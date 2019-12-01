using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Zealous.Models;

namespace Zealous.Controllers
{
    public class EquipmentsController : Controller
    {
        private ZealousContext db = new ZealousContext();
        const string SessionEquipCart = "equipCart";

        // GET: Equipments
        public ActionResult Index()
        {
            return View(db.Equipments.ToList());
        }

        [HttpGet]
        public ActionResult Book()
        {
            var equips = db.Equipments.OrderBy(x => x.EquipmentName).ToList();
            var bookedIds = Session[SessionEquipCart] as List<int>;
            FillBooking(equips, bookedIds);

            return View(equips);
        }

        private static void FillBooking(List<Equipment> equips, List<int> bookedIds)
        {
            if (bookedIds != null)
            {
                foreach (var e in equips)
                {
                    e.IsBooked = bookedIds.Contains(e.Id);
                }
            }
        }

        [HttpGet]
        public ActionResult BookOne(int equipId)
        {
            var bookedIds = Session[SessionEquipCart] as List<int>;
            if (bookedIds != null)
            {
                if (!bookedIds.Contains(eventId))
                {
                    bookedIds.Add(eventId);
                }
            }
            else
            {
                bookedIds = new List<int>() { eventId };
                Session[SessionEquipCart] = bookedIds;
            }
            var equips = db.Equipments.OrderBy(x => x.EquipmentName).ToList();
            FillBooking(equips, bookedIds);
            ViewBag.EquipId = eventId;
            return View("Book", equips);
        }

        [HttpGet]
        public ActionResult RemoveBooking(int equipId)
        {
            var bookedIds = Session[SessionEquipCart] as List<int>;
            if (bookedIds != null)
                bookedIds.Remove(equipId);

            var equips = db.Equipments.OrderBy(x => x.EquipmentName).ToList();
            FillBooking(equips, bookedIds);
            ViewBag.EquipId = equipId;
            return View("Book", equips);
        }


        // GET: Equipments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // GET: Equipments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Equipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EquipmentName,EquipmentDetail")] Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                db.Equipments.Add(equipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipment);
        }

        // GET: Equipments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // POST: Equipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EquipmentName,EquipmentDetail")] Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipment);
        }

        // GET: Equipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // POST: Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipment equipment = db.Equipments.Find(id);
            db.Equipments.Remove(equipment);
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
