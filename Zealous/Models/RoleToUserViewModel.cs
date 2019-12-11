using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zealous.Models
{
    public class RoleToUserViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}