using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using OrderProcessingConsoleApp.Interfaces;
using OrderProcessingConsoleApp.Models;
using OrderProcessingConsoleApp.Models.Country;
using OrderProcessingConsoleApp.Models.Part;
using OrderProcessingConsoleApp.Models.Request;
using OrderProcessingConsoleApp.Validators;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OrderProcessingConsoleAppTests.Validators
{
    public class OrderRequestValidatorTests
    {
       private readonly OrderRequestValidator _validator;
        private readonly Mock<IDirectoryService> _directoryService = new Mock<IDirectoryService>();
        private readonly Mock<IConverterService> _converterService = new Mock<IConverterService>();

        public OrderRequestValidatorTests()
        {
            _validator = new OrderRequestValidator(_directoryService.Object, _converterService.Object);

           _directoryService.Setup(ds => ds.GetFilePath(It.IsAny<string>()))
                .Returns("fegreg");

            _converterService.Setup(cs => cs.ConvertCountriesFromFile(It.IsAny<string>()))
                .Returns(CreateCountryList());

            _converterService.Setup(cs => cs.ConvertPartsListFromFile(It.IsAny<string>()))
                .Returns(CreatePartsList());
        }

        #region Setup
         
        private OrderRequest CreateOrderRequest()
        {
            return new OrderRequest
            {
                OrderAddress = new OrderAddress
                {
                    Country = "TestCountry"
                },
                RequestedParts = new List<RequestedPart>
                {
                    new RequestedPart
                    {
                        PartNumber = "TEST-PART-1",
                        Quantity = 5
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
                    PartNumber = "TEST-PART-1",
                    Price = 5
                }
            };
        }

        private List<CountryItem> CreateCountryList()
        {
            return new List<CountryItem>
            {
                new CountryItem
                {
                    CountryName = "TestCountry",
                    VatPercent = 20
                }
            };
        }

        #endregion

        [Fact]
        public void Validate_ExtendsAbstractValidorClass()
        {
            Assert.IsAssignableFrom<AbstractValidator<OrderRequest>>(_validator);
        }

        [Fact]
        public void Validate_OrderAddressEmpty_CorrectErrorMessage()
        {
            var request = CreateOrderRequest();
            request.OrderAddress = null;

            var validationResults = _validator.TestValidate(request);

            validationResults.ShouldNotHaveValidationErrorFor(r => r.RequestedParts);

            validationResults.ShouldHaveValidationErrorFor(r => r.OrderAddress)
                .WithErrorMessage("'Order Address' must not be empty.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Validate_OrderAddressCountryEmpty_CorrectErrorMessage(string countryValue)
        {
            var request = CreateOrderRequest();
            request.OrderAddress.Country = countryValue;

            var validationResults = _validator.TestValidate(request);

            validationResults.ShouldHaveValidationErrorFor(r => r.OrderAddress.Country)
                .WithErrorMessage("'Country' must not be empty.");
        }

        [Fact]
        public void Validate_OrderAddressCountryInvalid_CorrectErrorMessage()
        {
            var request = CreateOrderRequest();
            request.OrderAddress.Country = "Mauritius";

            var validationResults = _validator.TestValidate(request);

            validationResults.ShouldHaveValidationErrorFor(r => r.OrderAddress.Country)
                .WithErrorMessage("Could not find a VAT record for 'Mauritius'.");
        }

        [Fact]
        public void Validate_RequestedPartsEmpty_CorrectErrorMessage()
        {
            var request = CreateOrderRequest();
            request.RequestedParts = null;

            var validationResults = _validator.TestValidate(request);

            validationResults.ShouldNotHaveValidationErrorFor(r => r.OrderAddress);

            validationResults.ShouldHaveValidationErrorFor(r => r.RequestedParts)
                .WithErrorMessage("'Requested Parts' must not be empty.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Validate_RequestedPartEmpty_CorrectErrorMessage(string partValue)
        {
            var request = CreateOrderRequest();
            request.RequestedParts.FirstOrDefault().PartNumber = partValue;

            var expectedMessage = "'Part Number' must not be empty.";

            var validationResults = _validator.TestValidate(request);

            var error = Assert.Single(validationResults.Errors);

            Assert.Equal(expectedMessage, error.ErrorMessage);
        }

        [Fact]
        public void Validate_RequestedPartInvalid_CorrectErrorMessage()
        {
            var request = CreateOrderRequest();
            request.RequestedParts.FirstOrDefault().PartNumber = "INV-1";

            var expectedMessage = "'INV-1' is an invalid Part Number.";

            var validationResults = _validator.TestValidate(request);

            var error = Assert.Single(validationResults.Errors);

            Assert.Equal(expectedMessage, error.ErrorMessage);
        }

        [Fact]
        public void Validate_ValidRequest_NoErrorMessages()
        {
            var request = CreateOrderRequest();

            var validationResults = _validator.TestValidate(request);

            Assert.Empty(validationResults.Errors);
        }
    }
}
