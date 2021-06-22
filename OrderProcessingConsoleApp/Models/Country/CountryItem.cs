using System.Text.Json.Serialization;

namespace OrderProcessingConsoleApp.Models.Country
{
    public class CountryItem
    {
        public string CountryName { get; set; }

        public string CountryCode { get; set; }

        [JsonPropertyName("VATPercent")]
        public float VatPercent { get; set; }
    }
}
