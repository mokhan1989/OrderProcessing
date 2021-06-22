using OrderProcessingConsoleApp.Models;

namespace OrderProcessingConsoleApp.Interfaces
{
    public interface IValidationService
    {
        void ValidateOrderRequest(OrderRequest orderRequest);
    }
}
