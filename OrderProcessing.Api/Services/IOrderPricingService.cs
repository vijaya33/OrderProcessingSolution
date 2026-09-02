using OrderProcessing.Shared.Models;
namespace OrderProcessing.Api.Services;
public interface IOrderPricingService { OrderSummary Calculate(Order order); }
