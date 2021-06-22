using FluentValidation;
using OrderProcessingConsoleApp.Interfaces;
using OrderProcessingConsoleApp.Models.Request;
using OrderProcessingConsoleApp.Shared.Constants;
using System.Linq;

namespace OrderProcessingConsoleApp.Validators
{
    public class OrderAddressValidator : AbstractValidator<OrderAddress>
    {
        private readonly IDirectoryService _directoryService;
        private readonly IConverterService _converterService;

        public OrderAddressValidator(IDirectoryService directoryService, IConverterService converterService)
        {
            _directoryService = directoryService;
            _converterService = converterService;

            CascadeMode = CascadeMode.Stop;

            RuleFor(orderAddress => orderAddress.Country)
                .NotEmpty()
                .Custom((name, context) => {
                    if (!IsValidCountry(name))
                    {
                        context.AddFailure($"Could not find a VAT record for '{context.InstanceToValidate.Country}'.");
                    }
                });
        }

        private bool IsValidCountry(string countryName)
        {
            var countriesListFile = _directoryService.GetFilePath(Constants.CountryListFileName);
            var existingcountriesList = _converterService.ConvertCountriesFromFile(countriesListFile);
            var countryItem = existingcountriesList?.FirstOrDefault(c => c.CountryName == countryName);

            return countryItem != null;
        }
    }
}
