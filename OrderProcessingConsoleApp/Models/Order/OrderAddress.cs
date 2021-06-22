namespace OrderProcessingConsoleApp.Models.Request
{
    public class OrderAddress
    {
        public string CompanyName { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string PostCode { get; set; }

        public string Country { get; set; }
    }
}
