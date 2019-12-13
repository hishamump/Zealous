using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zealous.Models;

namespace Zealous.Controllers
{
    public class SuppliersController : ZealousController
    {
        // GET: Supplier
        [HttpGet]
        [Authorize(Roles = "Supplier")]
        public ActionResult Status()
        {
            return View();
        }

        [Authorize(Roles = "Supplier")]
        public ActionResult AddStatus()
        {
            return View();
        }

        [Authorize(Roles = "Supplier")]
        public ActionResult UpdateStatus()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Rank()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Rank(Supplier model)
        {
            //var supplier = db.s
            return View();
        }

    }
}