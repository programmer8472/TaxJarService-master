// Written by Alex Sheinton

using System;
using TaxJarService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TaxService.Tests
{
    [TestClass]
    public class UnitTest1
    {
        ITaxService taxService = null;

        public UnitTest1()
        {
            string client = "ABCCorp";

            if (client == "ABCCorp")
                taxService = new JarTaxService();
            //else if (client == "SomeOtherClient")
                //taxService = "SomeOtherClient";
        }

        [TestMethod]
        public void TestGetTaxRateForLocation()
        {
            Rate rate = new Rate { Zip = "33647" };
            var taxRateForLocation = taxService.GetTaxRateForLocation(rate);

            //Existance check
            Assert.IsNotNull(taxRateForLocation);

            //Expected values check
            Assert.IsTrue(taxRateForLocation.City == "TAMPA");
            Assert.IsTrue(taxRateForLocation.County == "HILLSBOROUGH");
            Assert.IsTrue(taxRateForLocation.Country == "US");
            Assert.IsTrue(taxRateForLocation.State == "FL");
            Assert.IsTrue(taxRateForLocation.CountyRate == 0.025M);
            Assert.IsTrue(taxRateForLocation.StateRate == 0.06M);
        }

        [TestMethod]
        public void TestTaxRateForOrder()
        {
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

            //Existance check
            Assert.IsNotNull(taxRateForOrder);

            //Expected values check
            Assert.IsTrue(taxRateForOrder.OrderTotalAmount == 16.5M);
            Assert.IsTrue(taxRateForOrder.Shipping == 1.5M);
            Assert.IsTrue(taxRateForOrder.AmountToCollect == 1.43M);
            Assert.IsTrue(taxRateForOrder.Rate == 0.095M);

        }


    }
}
