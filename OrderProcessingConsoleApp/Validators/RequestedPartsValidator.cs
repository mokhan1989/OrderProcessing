using FluentValidation;
using OrderProcessingConsoleApp.Interfaces;
using OrderProcessingConsoleApp.Models.Request;
using OrderProcessingConsoleApp.Shared.Constants;
using System.Linq;

namespace OrderProcessingConsoleApp.Validators
{
    public class RequestedPartsValidator : AbstractValidator<RequestedPart>
    {
        private readonly IDirectoryService _directoryService;
        private readonly IConverterService _converterService;

        public RequestedPartsValidator(IDirectoryService directoryService, IConverterService converterService)
        {
            _directoryService = directoryService;
            _converterService = converterService;

            CascadeMode = CascadeMode.Stop;

            RuleFor(requestedPart => requestedPart.PartNumber)
              .NotEmpty()
              .Custom((part, context) => {
                  if (!IsValidPartNumber(part))
                  {
                      context.AddFailure($"'{context.InstanceToValidate.PartNumber}' is an invalid Part Number.");
                  }
              });
        }

        private bool IsValidPartNumber(string partNumber)
        {
            var partsListFile = _directoryService.GetFilePath(Constants.PartsListFileName);
            var existingPartsList = _converterService.ConvertPartsListFromFile(partsListFile);
            var partItem = existingPartsList?.FirstOrDefault(p => p.PartNumber == partNumber);

            return partItem != null;
        }
    }
}
