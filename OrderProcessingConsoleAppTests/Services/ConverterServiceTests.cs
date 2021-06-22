using Moq;
using Newtonsoft.Json;
using OrderProcessingConsoleApp.Interfaces;
using OrderProcessingConsoleApp.Models;
using OrderProcessingConsoleApp.Models.Country;
using OrderProcessingConsoleApp.Models.Part;
using OrderProcessingConsoleApp.Models.Request;
using OrderProcessingConsoleApp.Services;
using System.Collections.Generic;
using Xunit;

namespace OrderProcessingConsoleAppTests.Services
{
    public class ConverterServiceTests
    {
        private readonly Mock<IDirectoryService> _directoryService = new Mock<IDirectoryService>();
        private readonly ConverterService _converterService;

        public ConverterServiceTests()
        {
            _converterService = new ConverterService(_directoryService.Object);
        }

        #region Setup

        private static string GetOrderRequestText()
        {
            var orderRequest = new OrderRequest
            {
                OrderAddress = new OrderAddress
                {
                    CompanyName = "TestCompany",
                    AddressLine1 = "Address1",
                    AddressLine2 = "Address2",
                    City = "City",
                    PostCode = "Postcode",
                    Country = "France"
                },
                RequestedParts = new List<RequestedPart>
                {
                    new RequestedPart
                    {
                        PartNumber = "LMN-1",
                        Quantity = 8
                    }
                }
            };

            return JsonConvert.SerializeObject(orderRequest);
        }

        private static string GetPartsListText()
        {
            var partsList = new List<PartItem>
            {
                new PartItem
                {
                    PartNumber = "RFG-4",
                    PartGroup = "G",
                    Price = 15.00m
                },
                new PartItem
                {
                    PartNumber = "YEF-8",
                    PartGroup = "U",
                    Price = 20.00m
                }
            };

            return JsonConvert.SerializeObject(partsList);
        }

        private static string GetCountriesListText()
        {
            var countriesList = new List<CountryItem>
            {
                new CountryItem
                {
                    CountryName = "United Kingdom",
                    CountryCode = "UK",
                    VatPercent = 20
                }
            };

            return JsonConvert.SerializeObject(countriesList);
        }

        #endregion

        [Fact]
        public void ConverterService_ExtendsIConverterInterface()
        {
            Assert.IsAssignableFrom<IConverterService>(_converterService);
        }

        #region ConvertOrderRequestFromFile

        [Fact]
        public void ConvertOrderRequestFromFile_CallsGetFilePath_WithPartsListFileName()
        {
            _directoryService.Setup(ds => ds.ReadTextFileFromPath(It.IsAny<string>())).Returns(GetOrderRequestText());

            _converterService.ConvertOrderRequestFromFile("OrderRequest.json");

            _directoryService.Verify(ds => ds.ReadTextFileFromPath("OrderRequest.json"), Times.Once);
        }

        [Fact]
        public void ConvertOrderRequestFromFile_ReturnsOrderRequestType()
        {
            _directoryService.Setup(ds => ds.ReadTextFileFromPath(It.IsAny<string>())).Returns(GetOrderRequestText());

            var convertedOrderRequest = _converterService.ConvertOrderRequestFromFile("OrderRequest.json");

            Assert.IsType<OrderRequest>(convertedOrderRequest);
        }

        #endregion

        #region ConvertPartsListFromFile

        [Fact]
        public void ConvertPartsListFromFile_CallsGetFilePath_WithPartsListFileName()
        {
            _directoryService.Setup(ds => ds.ReadTextFileFromPath(It.IsAny<string>())).Returns(GetPartsListText());

            _converterService.ConvertPartsListFromFile("PartsList.json");

            _directoryService.Verify(ds => ds.ReadTextFileFromPath("PartsList.json"), Times.Once);
        }

        [Fact]
        public void ConvertPartsListFromFile_ReturnsPartsListType()
        {
            _directoryService.Setup(ds => ds.ReadTextFileFromPath(It.IsAny<string>())).Returns(GetPartsListText());

            var convertedPartsList = _converterService.ConvertPartsListFromFile("PartsList.json");

            Assert.IsType<List<PartItem>>(convertedPartsList);
        }

        #endregion

        #region ConvertCountriesFromFile

        [Fact]
        public void ConvertCountriesFromFile_CallsGetFilePath_WithCountriesListFileName()
        {
            _directoryService.Setup(ds => ds.ReadTextFileFromPath(It.IsAny<string>())).Returns(GetCountriesListText());

            _converterService.ConvertCountriesFromFile("CountryList.json");

            _directoryService.Verify(ds => ds.ReadTextFileFromPath("CountryList.json"), Times.Once);
        }

        [Fact]
        public void ConvertCountriesFromFile_ReturnsCountryListType()
        {
            _directoryService.Setup(ds => ds.ReadTextFileFromPath(It.IsAny<string>())).Returns(GetCountriesListText());

            var convertedCountriesList = _converterService.ConvertCountriesFromFile("CountryList.json");

            Assert.IsType<List<CountryItem>>(convertedCountriesList);
        }

        #endregion

    }
}
