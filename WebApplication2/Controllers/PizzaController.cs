using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    // changes the way this controller is accessed
    // route is a full route 
    // route prefix - for this class all of the methods fall underneath this prefix
    [RoutePrefix("orderapi/pizza")]
    public class PizzaController : ApiController
    {
        // add api endpoint
        // gets appended to the routeprefix on the class
        // can put on two separate lines or combine with route
        //[HttpPost]
        // httpresponsemessage- web api will resolve what you want it to turn into
        [HttpPost, Route("placeorder")]
        public HttpResponseMessage PlaceOrder(OrderDTO newOrder)
        {
                //take dto and map it onto one of our order objects
                //object initialization
                var order = new Order
                {
                    NumberOfPizzas = newOrder.NumberOfPizzas,
                    TypeOfPizza = newOrder.TypeOfPizza,
                    AddressForDelivery = newOrder.AddressForDelivery,
                    NameOfCustomer = newOrder.NameOfCustomer,
                    Cost = 10*newOrder.NumberOfPizzas
                };

                var orderService = new OrderPizzaService();
                var success = orderService.OrderPizza(order);

                if (success)
                    return Request.CreateResponse(HttpStatusCode.Created);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, 
                    "Could not process order, please try again later... "); 
        }

        // setup a connection to my pizzatime db
        [HttpGet,Route("{id}")]
        public HttpResponseMessage GetAllOrders(int id)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["PizzaTime"].ConnectionString))
            {
                db.Open();

                //creating an anonymous type
                //var x = new { id = id , Name = "John" };


                var orders = db.QueryFirst<Order>("select * from Orders where Id = @id", new { id });

                return Request.CreateResponse(HttpStatusCode.OK, orders);
            }
        }

        // setup a connection to my pizzatime db
        [HttpGet, Route("")]
        public HttpResponseMessage GetOrder(string firstLetter)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["PizzaTime"].ConnectionString))
            {
                db.Open();

                // Query for the first order object and creating an anonymous type

                var orders = db.QueryFirst<Order>("select *" +
                                                   "from Orders" +
                                                   "where TypeOfPizza = @firstLetter", new { firstLetter });

                return Request.CreateResponse(HttpStatusCode.OK, orders);
            }
        }
    }
}
