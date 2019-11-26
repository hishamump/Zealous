using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}
