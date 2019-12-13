using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Zealous.Models
{
    public class ZealousContext : DbContext
    {

        public DbSet<AspNetUsers> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventTracking> EventTrackings { get; set; }
        public DbSet<Booking> Bookings { get; set; }


        public ZealousContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<Zealous.Models.Equipment> Equipments { get; set; }
    }
}