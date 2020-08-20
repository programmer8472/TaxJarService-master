// Written by Alex Sheinton

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaxJarService;

namespace ConsoleAppTxSrv
{
    class Program
    {
        static void Main(string[] args)
        {
            ITaxService taxService = null;
            string client = "ABCCorp";

            if (client == "ABCCorp")
                taxService = new JarTaxService();
            //else if (client == "SomeOtherClient")
                //taxService = "SOmeOtherClient";

            Rate rate = new Rate { Zip = "33647" };
            var taxRateForLocation = taxService.GetTaxRateForLocation(rate);
            foreach (PropertyInfo property in taxRateForLocation.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                Console.WriteLine(property.Name + ": " + property.GetValue(taxRateForLocation));
            }

            Console.WriteLine();
            Console.WriteLine("===================================");
            Console.WriteLine();

            Order order = new Order
            {
                FromCountry = "US",
                FromZip = "92093",
                FromState = "CA",
                FromCity = "La Jolla",
                FromStreet = "9500 Gilman Drive",
                ToCountry = "US",
                ToZip = "90002",
                ToState = "CA",
                ToCity = "Los Angeles",
                ToStreet = "1335 E 103rd St",
                Amount = 15,
                Shipping = 1.5M,
                LineItems = new List<LineItem>
                {
                    {
                        new LineItem
                        {
                            Id = "1",
                            Quantity = 1,
                            ProductTaxCode = "20010",
                            UnitPrice = 15,
                            Discount = 0
                        }
                    }
                }
            };
            var taxRateForOrder = taxService.GetTaxRateForOrder(order);
            foreach (PropertyInfo property in taxRateForOrder.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                Console.WriteLine(property.Name + ": " + property.GetValue(taxRateForOrder));
            }

            Console.ReadLine();
            
        }
    }

    
}
