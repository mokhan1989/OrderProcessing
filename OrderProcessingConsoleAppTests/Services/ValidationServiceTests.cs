using FluentValidation;
using Moq;
using OrderProcessingConsoleApp.Interfaces;
using OrderProcessingConsoleApp.Models;
using OrderProcessingConsoleApp.Services;
using Xunit;

namespace OrderProcessingConsoleAppTests.Services
{
    public class ValidationServiceTests
    {
        private readonly Mock<IDirectoryService> _directoryService = new Mock<IDirectoryService>();
        private readonly Mock<IConverterService> _converterService = new Mock<IConverterService>();
        private readonly ValidationService _validationService;

        public ValidationServiceTests()
        {
            _validationService = new ValidationService(_directoryService.Object, _converterService.Object);
        }

        [Fact]
        public void ValidationService_ExtendsIValidationInterface()
        {
            Assert.IsAssignableFrom<IValidationService>(_validationService);
        }

        [Fact]
        public void ValidationService_InvalidObject_ThrowsValidationException()
        {
            Assert.Throws<ValidationException>(() => _validationService.ValidateOrderRequest(new OrderRequest()));
        }

        [Fact]
        public void ValidationService_InvalidObject_CorrectExceptionMessage()
        {
            var exception = Record.Exception(() => _validationService.ValidateOrderRequest(new OrderRequest()));

            var expectedMessage = "Validation of Order request failed.";

            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
