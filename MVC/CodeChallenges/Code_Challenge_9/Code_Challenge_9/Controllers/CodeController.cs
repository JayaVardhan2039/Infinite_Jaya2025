using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Code_Challenge_9.Models;

namespace Code_Challenge_9.Controllers
{
        public class CodeController : Controller
        {
            NorthwindEntities db = new NorthwindEntities();

            // GET: Code
            public ActionResult Index()
            {
                return View();
            }
        [HttpGet]
        public ActionResult CustomersInGermany()
        {
            return View(new List<Customer>());
        }

        [HttpPost]
        public ActionResult CustomersInGermany(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
            {
                ViewBag.Error = "Please enter a country name.";
                return View(new List<Customer>());
            }

            var customers = db.Customers
                              .Where(c => c.Country.Contains(country))
                              .ToList();

            if (!customers.Any())
            {
                ViewBag.Error = "No customers found in the specified country.";
            }

            return View(customers);
        }


        [HttpGet]
    public ActionResult CustomerByOrderId()
        {
            return View();
        }

        // POST: Code/CustomerByOrderId
        
        [HttpPost]
        public ActionResult CustomerByOrderId(string orderId)
        {
            if (string.IsNullOrWhiteSpace(orderId))
            {
                ViewBag.Error = "Order ID is required.";
                return View(new List<Customer>());
            }

            if (!int.TryParse(orderId, out int parsedOrderId))
            {
                ViewBag.Error = "Order ID must be a valid number.";
                return View(new List<Customer>());
            }

            var customers = db.Orders
                              .Where(o => o.OrderID == parsedOrderId)
                              .Select(o => o.Customer)
                              .ToList();

            if (customers == null || customers.Count == 0)
            {
                ViewBag.Error = "No customer found for the given Order ID.";
                return View(new List<Customer>());
            }

            return View(customers);
        }

    }


}