using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zealous.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        // GET: Report
        public ActionResult ClientPerEvents()
        {
            return View();
        }
        // GET: Report
        public ActionResult SupplierDetails()
        {
            return View();
        }
        // GET: Report
        public ActionResult EquipmentDetails()
        {
            return View();
        }
    }
}