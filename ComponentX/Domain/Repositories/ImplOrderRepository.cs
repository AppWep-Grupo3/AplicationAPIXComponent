using BackendXComponent.ComponentX.Domain.Models;
namespace BackendXComponent.ComponentX.Domain.Repositories;

public interface ImplOrderRepository
{
    Task<IEnumerable<Order>> ListAsync();
    
    Task AddAsync(Order order);
    
    Task<Order> FindByIdAsync(int id);
    
    Task<Order> FindUserIdAsync(int userId);
    
    Task<Order> FindByDateAsync(DateTime date);
    
    void Update (Order order);
    
    void Delete(Order order);
}