using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zealous.Models;

namespace Zealous.Controllers
{
    public class EquipmentController : ZealousController 
    {
        // GET: Equipment
        [HttpGet]
        [Authorize]
        public ActionResult equip()
        {
            return View();
        }
        public ActionResult BookEquipment()
        {
            return View();
        }
        public ActionResult AddEquipment()
        {
            return View();
        }
        public ActionResult DelEquipment()
        {
            return View();
        }
        public ActionResult UpdEquipment()
        {
            return View();
        }
    }
}