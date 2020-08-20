// Written by Alex Sheinton

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxjar;

namespace TaxJarService
{
    public class JarTaxService : ITaxService
    {
        TaxjarApi _client = null;
        public JarTaxService()
        {
            _client = new TaxjarApi(ConfigurationManager.AppSettings["TaxjarApiKey"]);
        }

        public RateResponseAttributes GetTaxRateForLocation(Rate rate)
        {
            return _client.RatesForLocation(rate.Zip);
        }

        public TaxResponseAttributes GetTaxRateForOrder(Order order)
        {
            var rates = _client.TaxForOrder(order);
            return rates;
        }



    }
}
