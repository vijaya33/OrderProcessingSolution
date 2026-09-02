using System.Collections.Concurrent;
using OrderProcessing.Shared.Models;
namespace OrderProcessing.Api.Services;
public sealed class InMemoryOrderRepository : IOrderRepository
{
    private readonly ConcurrentDictionary<Guid, Order> _orders = new();
    public InMemoryOrderRepository()
    {
        var sample = new Order { CustomerName = "Sample Customer", CustomerEmail = "customer@example.com",
            DiscountPercent = 10m, TaxPercent = 8.25m, ShippingAmount = 7.50m,
            Items = [new() { ProductName = "Keyboard", Quantity = 1, UnitPrice = 79.99m }, new() { ProductName = "Mouse", Quantity = 2, UnitPrice = 24.50m }] };
        _orders[sample.Id] = Clone(sample);
    }
    public Task<IReadOnlyCollection<Order>> GetAllAsync(CancellationToken ct = default) => Task.FromResult<IReadOnlyCollection<Order>>(_orders.Values.Select(Clone).OrderByDescending(x => x.CreatedUtc).ToArray());
    public Task<Order?> GetAsync(Guid id, CancellationToken ct = default) => Task.FromResult(_orders.TryGetValue(id, out var value) ? Clone(value) : null);
    public Task<Order> AddAsync(Order order, CancellationToken ct = default) { _orders[order.Id] = Clone(order); return Task.FromResult(Clone(order)); }
    public Task<bool> UpdateAsync(Order order, CancellationToken ct = default) { if (!_orders.ContainsKey(order.Id)) return Task.FromResult(false); _orders[order.Id] = Clone(order); return Task.FromResult(true); }
    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default) => Task.FromResult(_orders.TryRemove(id, out _));
    private static Order Clone(Order x) => new() { Id=x.Id, CustomerName=x.CustomerName, CustomerEmail=x.CustomerEmail, CreatedUtc=x.CreatedUtc, Status=x.Status, DiscountPercent=x.DiscountPercent, TaxPercent=x.TaxPercent, ShippingAmount=x.ShippingAmount, Items=x.Items.Select(i => new OrderItem { Id=i.Id, ProductName=i.ProductName, Quantity=i.Quantity, UnitPrice=i.UnitPrice }).ToList() };
}
