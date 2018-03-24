using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class OrderDTO
    {

        public string TypeOfPizza { get; set; }
        public int NumberOfPizzas { get; set; }
        public string AddressForDelivery { get; set; }
        public string NameOfCustomer { get; set; }
    }
}