namespace OrderProcessing.Shared.Models;
public sealed record OrderSummary(Guid OrderId, string CustomerName, OrderStatus Status,
    int TotalQuantity, decimal Subtotal, decimal DiscountAmount, decimal TaxableAmount,
    decimal TaxAmount, decimal ShippingAmount, decimal GrandTotal, DateTimeOffset CalculatedUtc);
