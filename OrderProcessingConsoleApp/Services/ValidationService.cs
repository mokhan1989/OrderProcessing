using FluentValidation;
using OrderProcessingConsoleApp.Interfaces;
using OrderProcessingConsoleApp.Models;
using OrderProcessingConsoleApp.Validators;
using System;

namespace OrderProcessingConsoleApp.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IDirectoryService _directoryService;
        private readonly IConverterService _converterService;

        public ValidationService(IDirectoryService directoryService, IConverterService converterService)
        {
            _directoryService = directoryService;
            _converterService = converterService;
        }

        public void ValidateOrderRequest(OrderRequest orderRequest)
        {
            OrderRequestValidator orderRequestValidator = new OrderRequestValidator(_directoryService, _converterService);

            var validationResults = orderRequestValidator.Validate(orderRequest);

            if (!validationResults.IsValid)
            {
                foreach (var failure in validationResults.Errors)
                {
                    Console.WriteLine(failure.ErrorMessage);
                }

                throw new ValidationException("Validation of Order request failed.");
            }
        }
    }
}
