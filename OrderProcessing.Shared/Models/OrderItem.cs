using System.ComponentModel.DataAnnotations;
namespace OrderProcessing.Shared.Models;
public sealed class OrderItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required, StringLength(100)] public string ProductName { get; set; } = string.Empty;
    [Range(1, 10000)] public int Quantity { get; set; } = 1;
    [Range(typeof(decimal), "0.01", "999999999")] public decimal UnitPrice { get; set; }
    public decimal LineTotal => decimal.Round(Quantity * UnitPrice, 2, MidpointRounding.AwayFromZero);
}
