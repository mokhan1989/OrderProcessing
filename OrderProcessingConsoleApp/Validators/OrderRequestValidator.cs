using FluentValidation;
using OrderProcessingConsoleApp.Interfaces;
using OrderProcessingConsoleApp.Models;

namespace OrderProcessingConsoleApp.Validators
{
    public class OrderRequestValidator : AbstractValidator<OrderRequest>
    {
        private readonly IDirectoryService _directoryService;
        private readonly IConverterService _converterService;

        public OrderRequestValidator(IDirectoryService directoryService, IConverterService converterService)
        {
            _directoryService = directoryService;
            _converterService = converterService;

            RuleFor(orderRequest => orderRequest.OrderAddress).NotEmpty();
            RuleFor(orderRequest => orderRequest.RequestedParts).NotEmpty();
            RuleFor(orderRequest => orderRequest.OrderAddress).SetValidator(new OrderAddressValidator(_directoryService, _converterService));
            RuleForEach(orderRequest => orderRequest.RequestedParts).SetValidator(new RequestedPartsValidator(_directoryService, _converterService));
        }
    }
}
