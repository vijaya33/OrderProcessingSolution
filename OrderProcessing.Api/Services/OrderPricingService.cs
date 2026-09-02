using OrderProcessing.Shared.Models;
namespace OrderProcessing.Api.Services;
public sealed class OrderPricingService : IOrderPricingService
{
    public OrderSummary Calculate(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);
        var subtotal = Money(order.Items.Sum(x => x.Quantity * x.UnitPrice));
        var discount = Money(subtotal * order.DiscountPercent / 100m);
        var taxable = Money(subtotal - discount);
        var tax = Money(taxable * order.TaxPercent / 100m);
        var shipping = Money(order.ShippingAmount);
        return new(order.Id, order.CustomerName, order.Status, order.Items.Sum(x => x.Quantity),
            subtotal, discount, taxable, tax, shipping, Money(taxable + tax + shipping), DateTimeOffset.UtcNow);
    }
    private static decimal Money(decimal value) => decimal.Round(value, 2, MidpointRounding.AwayFromZero);
}
