using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zealous.Models
{
    public class EventTracking
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int EventId { get; set; }
        public string CustomerId { get; set; }
        public string OrganizerId { get; set; }
        public string SupplierId { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public byte EventStatus { get; set; }
    }
}