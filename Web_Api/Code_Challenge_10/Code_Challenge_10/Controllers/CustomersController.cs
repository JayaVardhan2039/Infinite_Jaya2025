using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Code_Challenge_10.Models;

namespace Code_Challenge_10.Controllers
{
    [RoutePrefix("api/Customers")]
    public class CustomersController : ApiController
    {
        NorthwindEntities db = new NorthwindEntities();

        // GET: api/Customers/ByCountry?country=Germany
        [HttpGet]
        [Route("ByCountry")]
        public IHttpActionResult GetCustomersByCountry(string country)
        {
            var result = db.GetCustomersByCountry(country).ToList();

            if (result == null || result.Count == 0)
                return NotFound();

            return Ok(result);
        }
    }
}
