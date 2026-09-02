namespace OrderProcessing.Shared.Models;
public sealed record OrderResponse(Order Order, OrderSummary Summary);
