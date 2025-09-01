using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Code_Challenge_10.Models;


namespace Code_Challenge_10.Controllers
{

    [RoutePrefix("api/Country")]
    public class CountryController : ApiController
    {
        // In-memory list of countries
        static List<Country> countryList = new List<Country>()
        {
            new Country { ID = 1, CountryName = "India", Capital = "New Delhi" },
            new Country { ID = 2, CountryName = "USA", Capital = "Washington, D.C." },
            new Country { ID = 3, CountryName = "Japan", Capital = "Tokyo" }
        };

        // GET: api/Country/All
        [HttpGet]
        [Route("All")]
        public IHttpActionResult GetAllCountries()
        {
            return Ok(countryList);
        }

        // GET: api/Country/ById?id=1
        [HttpGet]
        [Route("ById")]
        public IHttpActionResult GetCountryById(int id)
        {
            var country = countryList.FirstOrDefault(c => c.ID == id);
            if (country == null)
                return NotFound();
            return Ok(country);
        }

        // POST: api/Country/Add
        [HttpPost]
        [Route("Add")]
        public IHttpActionResult AddCountry([FromBody] Country country)
        {
            countryList.Add(country);
            return Created($"/api/Country/ById?id={country.ID}", country);
        }

        // PUT: api/Country/Update?id=1
        [HttpPut]
        [Route("Update")]
        public IHttpActionResult UpdateCountry(int id, [FromBody] Country updatedCountry)
        {
            var country = countryList.FirstOrDefault(c => c.ID == id);
            if (country == null)
                return NotFound();

            country.CountryName = updatedCountry.CountryName;
            country.Capital = updatedCountry.Capital;
            return Ok(country);
        }

        // DELETE: api/Country/Delete?id=1
        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult DeleteCountry(int id)
        {
            var country = countryList.FirstOrDefault(c => c.ID == id);
            if (country == null)
                return NotFound();

            countryList.Remove(country);
            return Ok();
        }
    }
}