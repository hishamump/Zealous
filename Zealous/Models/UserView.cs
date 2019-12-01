using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zealous.Models
{
    public class AspNetUsers
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public byte Status { get; set; }
        [NotMapped]
        public string StatusName { get; set; }
    }
}