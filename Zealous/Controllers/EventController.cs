using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zealous.Models;

namespace Zealous.Controllers
{
    public class EventController : ZealousController
    {

        
        [HttpGet]
        [Authorize]

       
        public ActionResult Index()
        {
            var events = db.Events.OrderBy(x => x.EventName).ToList();
            return View(events);
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

        private void FillBooked()
        {

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
            return View("Book" ,events);
        }




    }
}