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
        public ActionResult Index()
        {
            string userId = null;
            if (!User.IsInRole(UserRole.Admin.ToString())
                && !User.IsInRole(UserRole.Supplier.ToString())
                && !User.IsInRole(UserRole.Organizer.ToString()))
                userId = User.Identity.GetUserId();
            var events = GetBookedEvents(userId, null, null);
            return View(events);
        }

        private List<Booking> GetBookedEvents(string userId, DateTime? startDate, DateTime? endDate)
        {
            var query = db.Bookings.Where(b => true);
            if (startDate != null && endDate != null)
                query = query.Where(b => b.BookingDate >= startDate.Value && b.BookingDate <= endDate.Value);
            if (userId != null)
                query = query.Where(b => b.UserId == userId);

            return query.Join(db.Events, b => b.EventId, e => e.Id, (b, e) => new
            {
                b.Id,
                b.BookingDate,
                b.BookingStatus,
                b.EquipmentId,
                b.EventId,
                b.UserId,
                e.EventName
            }).AsEnumerable().Select(b => new Booking {
                Id = b.Id,
                BookingDate = b.BookingDate,
                BookingStatus = b.BookingStatus,
                EquipmentId = b.EquipmentId,
                EventId = b.EventId,
                UserId = b.UserId,
                EventName = b.EventName
            }).ToList();
        }
        // Track one event progress
        public ActionResult ProgressDetail(int id)
        {
            //Collect progress data for current event
            var eventProgress = db.EventTrackings.Where(e => e.BookingId == id).ToList();
            var eventId = eventProgress.First().EventId;
            var evnt = db.Events.FirstOrDefault(e => e.Id == eventId);

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
            var eventTracking = db.EventTrackings.AsEnumerable().LastOrDefault(et => et.BookingId == id);
            var evnt = db.Events.FirstOrDefault(e => e.Id == eventTracking.EventId);
            var detail = new ProgressDetail();
            detail.EventId = eventTracking.EventId;
            detail.BookingId = id;
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
            var currentTrack = db.EventTrackings.Where(e => e.BookingId == details.BookingId).OrderByDescending(e => e.Id).FirstOrDefault();
            if (currentTrack != null && currentTrack.EventStatus == details.EventStatus)
                return View(GetProgressDetail(details.BookingId));
            var track = new EventTracking { CustomerId = User.Identity.GetUserId(), BookingId = details.BookingId, EventId = details.EventId, EventStatus = details.EventStatus, Date = DateTime.Now };
            db.EventTrackings.Add(track);

            var booking = db.Bookings.Where(b => b.Id == details.BookingId).FirstOrDefault();
            if (booking != null)
                booking.BookingStatus = ((EventStatus)details.EventStatus).ToString();
            db.SaveChanges();
            ViewBag.Updated = true;
            return View(GetProgressDetail(details.BookingId));
        }


    }
}