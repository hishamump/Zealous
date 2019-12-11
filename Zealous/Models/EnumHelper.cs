using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zealous.Models
{
    public static class EnumHelper
    {
        public static List<ValueTextView> GetEventStatusView()
        {
            return ((IEnumerable<EventStatus>)Enum.GetValues(typeof(EventStatus)))
                    .Select(c => new ValueTextView() { Value = (byte)c, Text = c.ToString() })
                    .ToList();
        }

        public static List<ValueTextView> GetUserRoleView()
        {
            return ((IEnumerable<UserRole>)Enum.GetValues(typeof(UserRole)))
                    .Select(c => new ValueTextView() { Value = (byte)c, Text = c.ToString() })
                    .ToList();
        }
    }
}