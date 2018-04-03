using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class OrderPizzaService
    {
        public bool OrderPizza(Order order)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["PizzaTime"].ConnectionString))
            {
                db.Open();

                var numberAffected = db.Execute("Insert into Orders(TypeOfPizza, NumberOfPizzas, Cost, AddressForDelivery, NameOfCustomer)" +
                               "Values(@TypeOfPizza, @NumberOfPizzas, @Cost, @AddressForDelivery, @NameOfCustomer)",
                                order);

                return numberAffected == 1;
            }
        }
    }
}