using Newtonsoft.Json;
using OrderProcessingConsoleApp.Interfaces;
using OrderProcessingConsoleApp.Models;
using OrderProcessingConsoleApp.Models.Country;
using OrderProcessingConsoleApp.Models.Part;
using System.Collections.Generic;

namespace OrderProcessingConsoleApp.Services
{
    public class ConverterService : IConverterService
    {
        private readonly IDirectoryService _directoryService;

        public ConverterService(IDirectoryService directoryService)
        {
            _directoryService = directoryService;
        }

        public OrderRequest ConvertOrderRequestFromFile(string filePath)
        {
            var fileText = _directoryService.ReadTextFileFromPath(filePath);

            return JsonConvert.DeserializeObject<OrderRequest>(fileText);
        }

        public List<PartItem> ConvertPartsListFromFile(string filePath)
        {
            var fileText = _directoryService.ReadTextFileFromPath(filePath);

            return JsonConvert.DeserializeObject<List<PartItem>>(fileText);
        }

        public List<CountryItem> ConvertCountriesFromFile(string filePath)
        {
            var fileText = _directoryService.ReadTextFileFromPath(filePath);

            return JsonConvert.DeserializeObject<List<CountryItem>>(fileText); ;
        }
    }
}
