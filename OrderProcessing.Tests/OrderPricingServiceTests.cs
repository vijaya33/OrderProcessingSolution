using OrderProcessing.Api.Services; using OrderProcessing.Shared.Models;
using Xunit;
namespace OrderProcessing.Tests;

public sealed class OrderPricingServiceTests
{
 [Fact] public void Calculate_AppliesDiscountThenTaxThenShipping(){var order=new Order{CustomerName="Test",DiscountPercent=10m,TaxPercent=5m,ShippingAmount=7m,Items=[new(){ProductName="A",Quantity=2,UnitPrice=50m}]};var result=new OrderPricingService().Calculate(order);Assert.Equal(100m,result.Subtotal);Assert.Equal(10m,result.DiscountAmount);Assert.Equal(4.50m,result.TaxAmount);Assert.Equal(101.50m,result.GrandTotal);}
 [Fact] public void Calculate_UsesDecimalRounding(){var order=new Order{CustomerName="Test",TaxPercent=8.25m,Items=[new(){ProductName="A",Quantity=3,UnitPrice=0.10m}]};var result=new OrderPricingService().Calculate(order);Assert.Equal(0.02m,result.TaxAmount);Assert.Equal(0.32m,result.GrandTotal);}
}
