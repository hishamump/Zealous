using Microsoft.AspNet.Identity;
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
    public class EventsController : Controller
    {
        private ZealousContext db = new ZealousContext();

        // GET: Events
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }

        [HttpGet]
        public ActionResult ConfirmBooking()
        {
            var bookedEventIds = Session["cart"] as List<int>;
            if (bookedEventIds != null)
            {
                foreach (var id in bookedEventIds)
                {
                    var booking = new Booking {
                        EventId = id,
                        UserId = User.Identity.GetUserId(),
                        BookingStatus = EventStatus.Create.ToString(),
                        BookingDate = DateTime.Now
                    };

                    db.Bookings.Add(booking);
                    db.SaveChanges();
                    //Keep EquipmentId into Booking table, we will not allow booking equipment without Event booked first.
                    //The record of Event booking should be with EquipmentId = null,
                    // then every equipment booking should have new record with EventId been set.

                    var eventTrack = new EventTracking
                    {
                        BookingId = booking.Id,
                        EventId = id,
                        CustomerId = User.Identity.GetUserId(),
                        EventStatus = (byte)EventStatus.Create,
                        Date = DateTime.Now
                    };
                    db.EventTrackings.Add(eventTrack);
                }
            }
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult Book()
        {
            var events = db.Events.OrderBy(x => x.EventName).ToList();
            var bookedEventIds = Session["cart"] as List<int>;
            FillBooking(events, bookedEventIds);

            return View(events);
        }

        private static void FillBooking(List<Event> events, List<int> bookedEventIds)
        {
            if (bookedEventIds != null)
            {
                foreach (var e in events)
                {
                    e.IsBooked = bookedEventIds.Contains(e.Id);
                }
            }
        }

        [HttpGet]
        public ActionResult BookOne(int eventId)
        {
            var bookedEventIds = Session["cart"] as List<int>;
            if (bookedEventIds != null)
            {
                if (!bookedEventIds.Contains(eventId))
                {
                    bookedEventIds.Add(eventId);
                }
            }
            else
            {
                bookedEventIds = new List<int>() { eventId };
                Session["cart"] = bookedEventIds;
            }
            var events = db.Events.OrderBy(x => x.EventName).ToList();
            FillBooking(events, bookedEventIds);
            ViewBag.EventId = eventId;
            return View("Book", events);
        }

        [HttpGet]
        public ActionResult RemoveBooking(int eventId)
        {
            var bookedEventIds = Session["cart"] as List<int>;
            if (bookedEventIds != null)
                bookedEventIds.Remove(eventId);

            var events = db.Events.OrderBy(x => x.EventName).ToList();
            FillBooking(events, bookedEventIds);
            ViewBag.EventId = eventId;
            return View("Book", events);
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EventName,Description,Image")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EventName,Description,Image")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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
