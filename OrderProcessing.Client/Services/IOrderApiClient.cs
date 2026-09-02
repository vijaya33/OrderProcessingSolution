using OrderProcessing.Shared.Models;
namespace OrderProcessing.Client.Services;
public interface IOrderApiClient { Task<IReadOnlyList<OrderResponse>> GetAllAsync(CancellationToken ct=default); Task<OrderResponse> GetAsync(Guid id,CancellationToken ct=default); Task<OrderResponse> CreateAsync(Order order,CancellationToken ct=default); Task<OrderResponse> UpdateAsync(Order order,CancellationToken ct=default); Task DeleteAsync(Guid id,CancellationToken ct=default); }
