using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Code_Challenge_10.Models;


namespace Code_Challenge_10.Controllers
{
[RoutePrefix("api/Orders")]
public class OrdersController : ApiController
{
    NorthwindEntities db = new NorthwindEntities();

    [HttpGet]
    [Route("ByEmployee/{employeeId:int}")]
    public IHttpActionResult GetOrdersByEmployee(int employeeId)
    {
        var orders = db.Orders
                       .Where(o => o.EmployeeID == employeeId)
                       .Select(o => new
                       {
                           o.OrderID,
                           o.CustomerID,
                           o.EmployeeID,
                           o.OrderDate,
                           o.RequiredDate,
                           o.ShippedDate,
                           o.ShipVia,
                           o.Freight,
                           o.ShipName,
                           o.ShipAddress,
                           o.ShipCity,
                           o.ShipRegion,
                           o.ShipPostalCode,
                           o.ShipCountry
                       }).ToList();

        return Ok(orders);
    }
}
}
