using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Domain.Services.Communication.Communication;
namespace BackendXComponent.ComponentX.Domain.Services.Communication;
public interface ImplOrderService
{
    Task<IEnumerable<Order>> ListAsync();
    Task<OrderResponse> SaveAsync(Order order);
    Task<OrderResponse> UpdateAsync(int id, Order order);
    Task<OrderResponse> DeleteAsync(int id);
}