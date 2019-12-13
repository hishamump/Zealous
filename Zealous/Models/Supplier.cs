using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zealous.Models
{
    public class Supplier
    {
        public String UserId { set; get; }
        public String SupplierId { set; get; }
        public int Rank { get; set; }
        public string Description { get; set; }
    }
}