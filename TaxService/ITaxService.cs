using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxjar;

namespace TaxJarService
{
    public interface ITaxService
    {
        RateResponseAttributes GetTaxRateForLocation(Rate rate);
        TaxResponseAttributes GetTaxRateForOrder(Order order);
    }
}
