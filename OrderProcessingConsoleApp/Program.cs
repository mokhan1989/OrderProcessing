using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using Microsoft.Extensions.DependencyInjection;
using OrderProcessingConsoleApp.Interfaces;
using OrderProcessingConsoleApp.Services;
using OrderProcessingConsoleApp.Shared.Constants;

namespace OrderProcessingConsoleApp
{
    public class Program
    {
        private readonly IConverterService _converterService;
        private readonly IDirectoryService _directoryService;
        private readonly IValidationService _validationService;
        private readonly ICalculationService _calculationService;

        public Program(IConverterService converterService, IDirectoryService directoryService,
            IValidationService validationService, ICalculationService calculationService)
        {
            _converterService = converterService;
            _directoryService = directoryService;
            _validationService = validationService;
            _calculationService = calculationService;
        }

        static void Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();
                host.Services.GetRequiredService<Program>().Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured while running the application: {ex.Message}");
                Console.ReadKey();
            }

            Console.ReadKey();
        }

        public void Run()
        {
            var orderRequestFile = _directoryService.GetFilePath(Constants.OrderRequestFileName);
            var partsListFile = _directoryService.GetFilePath(Constants.PartsListFileName);
            var countriesListFile = _directoryService.GetFilePath(Constants.CountryListFileName);

            var orderRequestObject = _converterService.ConvertOrderRequestFromFile(orderRequestFile);
            var partsListObject = _converterService.ConvertPartsListFromFile(partsListFile);
            var countriesListObject = _converterService.ConvertCountriesFromFile(countriesListFile);

            Console.WriteLine("Validating Order request file...");

            _validationService.ValidateOrderRequest(orderRequestObject);

            Console.WriteLine("Order request is valid. Calculating the Order Invoice...");

            var orderInvoice = _calculationService.CalculateOrderInvoice(orderRequestObject, partsListObject, countriesListObject);

            Console.WriteLine("Here is your invoice:");
            Console.WriteLine(JsonConvert.SerializeObject(orderInvoice, Formatting.Indented));
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddScoped<Program>();
                    services.AddScoped<IDirectoryService, DirectoryService>();
                    services.AddScoped<IConverterService, ConverterService>();
                    services.AddScoped<IValidationService, ValidationService>();
                    services.AddScoped<ICalculationService, CalculationService>();
                });
        }
    }
}
