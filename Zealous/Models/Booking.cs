using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Zealous.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public string BookingStatus { get; set; }
        public int EquipmentId { get; set; }
        public int EventId { get; set; }
        public string UserId { get; set; }
        [NotMapped]
        public string EventName { get; set; }
    }
}
