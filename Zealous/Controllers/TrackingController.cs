using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zealous.Models;
using Microsoft.AspNet.Identity;

namespace Zealous.Controllers
{
    [Authorize]
    public class TrackingController : ZealousController
    {
        public ActionResult EventList()
        {
            var events = db.Events.ToList();
            return View(events);
        }

        // Track one event progress
        public ActionResult ProgressDetail(int id)
        {
            //Collect progress data for current event
            var eventProgress = db.EventTrackings.Where(e => e.EventId == id).ToList();
            var evnt = db.Events.FirstOrDefault(e => e.Id == id);

            //
            var eList = new List<ProgressDetail>();
            foreach (var e in eventProgress)
                eList.Add(new ProgressDetail {
                    Id = e.Id,
                    EventId = e.EventId,
                    CustomerId = e.CustomerId,
                    OrganizerId = e.OrganizerId,
                    SupplierId  = e.SupplierId,
                    Date  = e.Date,
                    Note = e.Note,
                    EventStatus = e.EventStatus,
                    EventName = evnt.EventName
            });
            return View(eList);
        }

        [HttpGet]
        public ActionResult UpdateEventProgress(int id)
        {
            return View(GetProgressDetail(id));
        }

        private ProgressDetail GetProgressDetail(int id) {
            //Get the tracking record
            var eventTracking = db.EventTrackings.AsEnumerable().LastOrDefault(et => et.EventId == id);
            var evnt = db.Events.FirstOrDefault(e => e.Id == id);
            var detail = new ProgressDetail();
            detail.Id = id;
            detail.EventName = evnt.EventName;
            detail.EventStatus = (byte)EventStatus.Create;

            //Get the actual saved event status and fill it in the model to return to view
            if (eventTracking != null)
                detail.EventStatus = eventTracking.EventStatus;
            return detail;
        }

        // Track one event progress
        [HttpPost]
        public ActionResult UpdateEventProgress(ProgressDetail details)
        {
            var track = new EventTracking { CustomerId = User.Identity.GetUserId(), EventId = details.Id, EventStatus = details.EventStatus, Date = DateTime.Now };
            db.EventTrackings.Add(track);
            db.SaveChanges();
            return View(GetProgressDetail(details.Id));
        }


    }
}