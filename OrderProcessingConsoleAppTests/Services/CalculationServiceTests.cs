using Newtonsoft.Json;
using OrderProcessingConsoleApp.Interfaces;
using OrderProcessingConsoleApp.Models;
using OrderProcessingConsoleApp.Models.Country;
using OrderProcessingConsoleApp.Models.Order;
using OrderProcessingConsoleApp.Models.Part;
using OrderProcessingConsoleApp.Models.Request;
using OrderProcessingConsoleApp.Services;
using System.Collections.Generic;
using Xunit;

namespace OrderProcessingConsoleAppTests.Services
{
    public class CalculationServiceTests
    {
        private readonly CalculationService _calculationService;
        private readonly OrderRequest _orderRequest;
        private readonly List<PartItem> _partsList;
        private readonly List<CountryItem> _countriesList;

        public CalculationServiceTests()
        {
            _calculationService = new CalculationService();
            _orderRequest = CreateOrderRequest();
            _partsList = CreatePartsList();
            _countriesList = CreateCountryList();
        }

        #region Setup

        private OrderRequest CreateOrderRequest()
        {
            return new OrderRequest
            {
                OrderAddress = new OrderAddress
                {
                    CompanyName = "TestCompany",
                    Country = "United Kingdom"
                },
                RequestedParts = new List<RequestedPart>
                {
                    new RequestedPart
                    {
                        PartNumber = "TEST-1",
                        Quantity = 2
                    },
                    new RequestedPart
                    {
                        PartNumber = "TEST-2",
                        Quantity = 2
                    }
                }
            };
        }

        private List<PartItem> CreatePartsList()
        {
            return new List<PartItem>
            {
                new PartItem
                {
                    PartNumber = "TEST-1",
                    Price = 10
                },
                new PartItem
                {
                    PartNumber = "TEST-2",
                    Price = 20.5m
                }
            };
        }

        private List<CountryItem> CreateCountryList()
        {
            return new List<CountryItem>
            {
                new CountryItem
                {
                    CountryName = "United Kingdom",
                    CountryCode = "UK",
                    VatPercent = 25
                }
            };
        }

        #endregion

        #region Tests

        [Fact]
        public void CalculationService_ExtendsICalculationService()
        {
            Assert.IsAssignableFrom<ICalculationService>(_calculationService);
        }

        [Fact]
        public void CalculationService_ReturnsOrderInvoiceType()
        {
            var orderInvoice = _calculationService.CalculateOrderInvoice(_orderRequest, _partsList, _countriesList);

            Assert.IsType<OrderInvoice>(orderInvoice);
        }

        [Fact]
        public void CalculationService_ReturnsOrderInvoiceObject()
        {
            var orderInvoice = JsonConvert.SerializeObject(_calculationService.CalculateOrderInvoice(_orderRequest, _partsList, _countriesList));

            var expectedInvoice = JsonConvert.SerializeObject(new OrderInvoice
            {
                CompanyName = "TestCompany",
                OrderTotal = 76.25m
            });

            Assert.Equal(expectedInvoice, orderInvoice);
        }

        #endregion
    }
}
