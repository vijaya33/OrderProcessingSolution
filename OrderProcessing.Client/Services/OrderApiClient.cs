using System.Net.Http.Json;
using OrderProcessing.Shared.Models;
namespace OrderProcessing.Client.Services;
public sealed class OrderApiClient(HttpClient http) : IOrderApiClient
{
 public async Task<IReadOnlyList<OrderResponse>> GetAllAsync(CancellationToken ct=default)=>await http.GetFromJsonAsync<List<OrderResponse>>("api/orders",ct) ?? [];
 public async Task<OrderResponse> GetAsync(Guid id,CancellationToken ct=default)=>await http.GetFromJsonAsync<OrderResponse>($"api/orders/{id}",ct) ?? throw new InvalidOperationException("Empty API response.");
 public Task<OrderResponse> CreateAsync(Order order,CancellationToken ct=default)=>SendAsync(HttpMethod.Post,"api/orders",order,ct);
 public Task<OrderResponse> UpdateAsync(Order order,CancellationToken ct=default)=>SendAsync(HttpMethod.Put,$"api/orders/{order.Id}",order,ct);
 public async Task DeleteAsync(Guid id,CancellationToken ct=default){using var response=await http.DeleteAsync($"api/orders/{id}",ct);response.EnsureSuccessStatusCode();}
 private async Task<OrderResponse> SendAsync(HttpMethod method,string uri,Order order,CancellationToken ct){using var response=await http.SendAsync(new HttpRequestMessage(method,uri){Content=JsonContent.Create(order)},ct);if(!response.IsSuccessStatusCode)throw new InvalidOperationException(await response.Content.ReadAsStringAsync(ct));return await response.Content.ReadFromJsonAsync<OrderResponse>(cancellationToken:ct) ?? throw new InvalidOperationException("Empty API response.");}
}
