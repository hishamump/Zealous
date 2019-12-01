using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zealous.Models
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }
        public String EquipmentName { set;get;}
        public String EquipmentDetail { set; get; }
        [NotMapped]
        public bool IsBooked { get; set; }
    }
}
