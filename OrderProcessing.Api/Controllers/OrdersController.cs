using Microsoft.AspNetCore.Mvc;
using OrderProcessing.Api.Services;
using OrderProcessing.Shared.Models;
namespace OrderProcessing.Api.Controllers;
[ApiController, Route("api/orders")]
public sealed class OrdersController(IOrderRepository repository, IOrderPricingService pricing) : ControllerBase
{
    [HttpGet] public async Task<ActionResult<IEnumerable<OrderResponse>>> GetAll(CancellationToken ct) => Ok((await repository.GetAllAsync(ct)).Select(ToResponse));
    [HttpGet("{id:guid}")] public async Task<ActionResult<OrderResponse>> Get(Guid id, CancellationToken ct) { var order=await repository.GetAsync(id,ct); return order is null ? NotFound() : Ok(ToResponse(order)); }
    [HttpPost] public async Task<ActionResult<OrderResponse>> Create([FromBody] Order order, CancellationToken ct)
    {
        Normalize(order, true); if (!TryValidateModel(order)) return ValidationProblem(ModelState);
        var saved=await repository.AddAsync(order,ct); return CreatedAtAction(nameof(Get),new { id=saved.Id },ToResponse(saved));
    }
    [HttpPut("{id:guid}")] public async Task<ActionResult<OrderResponse>> Update(Guid id,[FromBody] Order order,CancellationToken ct)
    {
        var existing=await repository.GetAsync(id,ct); if (existing is null) return NotFound();
        if (existing.Status is OrderStatus.Completed or OrderStatus.Cancelled) return Conflict(new ProblemDetails { Title="Order cannot be edited",Detail="Completed or cancelled orders are immutable." });
        order.Id=id; order.CreatedUtc=existing.CreatedUtc; Normalize(order,false); if (!TryValidateModel(order)) return ValidationProblem(ModelState);
        await repository.UpdateAsync(order,ct); return Ok(ToResponse((await repository.GetAsync(id,ct))!));
    }
    [HttpDelete("{id:guid}")] public async Task<IActionResult> Delete(Guid id,CancellationToken ct) => await repository.DeleteAsync(id,ct) ? NoContent() : NotFound();
    private OrderResponse ToResponse(Order order) => new(order,pricing.Calculate(order));
    private static void Normalize(Order order,bool isNew) { if(isNew){order.Id=Guid.NewGuid();order.CreatedUtc=DateTimeOffset.UtcNow;} order.CustomerName=order.CustomerName.Trim(); order.CustomerEmail=order.CustomerEmail?.Trim(); foreach(var item in order.Items){if(item.Id==Guid.Empty)item.Id=Guid.NewGuid();item.ProductName=item.ProductName.Trim();} }
}
