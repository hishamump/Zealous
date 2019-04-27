﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zealous.Models;

namespace Zealous.Controllers
{
    public class EventController : ZealousController
    {

        
        [HttpGet]
        [Authorize]

       
        public ActionResult Index()
        {
            var products = db.Products.OrderBy(x => x.Item_name).ToList();
            //product.Add(s);


            return View(products);
        }
        
        [HttpPost]
        public ActionResult Index(Product p)
        {
            if (ModelState.IsValid)
            {
                if (Session["cart"] != null)
                {
                    var ls = Session["cart"] as List<Product>;
                    ls.Add(p);
                }
                else
                {
                    Session["cart"] = new List<Product>() { p };
                }
              
                RedirectToAction("Index", "Home"); // Anti F5 submit
            }
            return View(); // model validate is false
        }

     



    }
}