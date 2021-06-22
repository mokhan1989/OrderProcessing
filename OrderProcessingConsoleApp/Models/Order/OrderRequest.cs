using OrderProcessingConsoleApp.Models.Request;
using System.Collections.Generic;

namespace OrderProcessingConsoleApp.Models
{
    public class OrderRequest
    {
        public OrderAddress OrderAddress { get; set; }

        public List<RequestedPart> RequestedParts { get; set; }
    }
}
