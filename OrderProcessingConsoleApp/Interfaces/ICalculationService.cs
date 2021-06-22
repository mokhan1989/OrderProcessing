using OrderProcessingConsoleApp.Models;
using OrderProcessingConsoleApp.Models.Country;
using OrderProcessingConsoleApp.Models.Order;
using OrderProcessingConsoleApp.Models.Part;
using System.Collections.Generic;

namespace OrderProcessingConsoleApp.Interfaces
{
    public interface ICalculationService
    {
        OrderInvoice CalculateOrderInvoice(OrderRequest orderRequest, List<PartItem> partItems, List<CountryItem> countryItems);
    }
}
