using OrderProcessingConsoleApp.Models;
using OrderProcessingConsoleApp.Models.Country;
using OrderProcessingConsoleApp.Models.Part;
using System.Collections.Generic;

namespace OrderProcessingConsoleApp.Interfaces
{
    public interface IConverterService
    {
        OrderRequest ConvertOrderRequestFromFile(string filePath);

        List<PartItem> ConvertPartsListFromFile(string filePath);

        List<CountryItem> ConvertCountriesFromFile(string filePath);
    }
}
