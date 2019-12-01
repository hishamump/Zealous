using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zealous.Models
{
    public enum EventStatus
    {
        Create,
        Pending,
        Approved,
        Wait
    }

    public class ValueTextView
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
}