using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zealous.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string EventName { get; set; }
        public string ProductId { get; set; }
        public string ProductDes { get; set; }
        public string Image { get; set; }
        public decimal Amount { get; set; }
    }
}
