using System.ComponentModel.DataAnnotations;
namespace OrderProcessing.Shared.Models;
public sealed class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required, StringLength(100)] public string CustomerName { get; set; } = string.Empty;
    [EmailAddress, StringLength(200)] public string? CustomerEmail { get; set; }
    public DateTimeOffset CreatedUtc { get; set; } = DateTimeOffset.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Draft;
    [Range(typeof(decimal), "0", "100")] public decimal DiscountPercent { get; set; }
    [Range(typeof(decimal), "0", "100")] public decimal TaxPercent { get; set; }
    [Range(typeof(decimal), "0", "999999999")] public decimal ShippingAmount { get; set; }
    [MinLength(1)] public List<OrderItem> Items { get; set; } = [];
}
