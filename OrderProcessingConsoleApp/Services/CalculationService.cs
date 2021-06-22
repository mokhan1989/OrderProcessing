using OrderProcessingConsoleApp.Interfaces;
using OrderProcessingConsoleApp.Models;
using OrderProcessingConsoleApp.Models.Country;
using OrderProcessingConsoleApp.Models.Order;
using OrderProcessingConsoleApp.Models.Part;
using System.Collections.Generic;
using System.Linq;

namespace OrderProcessingConsoleApp.Services
{
    public class CalculationService : ICalculationService
    {
        public OrderInvoice CalculateOrderInvoice(OrderRequest orderRequest, List<PartItem> partItems, List<CountryItem> countryItems)
        {
            var vatPercent = countryItems.FirstOrDefault(c => c.CountryName == orderRequest?.OrderAddress?.Country).VatPercent;

            var subTotalsList = orderRequest?.RequestedParts?.
                Select(rp => CalculateSubTotal(rp.PartNumber, rp.Quantity, partItems, vatPercent)).ToList();

            var billingTotal = ApplyVatToBillingTotal(subTotalsList, vatPercent);

            return new OrderInvoice
            {
                CompanyName = orderRequest?.OrderAddress?.CompanyName,
                OrderTotal = billingTotal
            };
        }

        private static decimal ApplyVatToBillingTotal(List<decimal> subTotals, float vat)
        {
            var total = subTotals.Sum();
            var totalWithVat = total / 100 * (decimal)vat + total;

            return totalWithVat;
        }

        private static decimal CalculateSubTotal(string partNumber, int quantity, List<PartItem> partsList, float vat)
        {
            var partItem = partsList?.FirstOrDefault(p => p.PartNumber == partNumber);
            var subTotal = quantity * partItem.Price;

            var subTotalWithVAT = subTotal / 100 * (decimal)vat + subTotal;

            return subTotal;
        }
    }
}
