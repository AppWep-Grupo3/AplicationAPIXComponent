using BackendXComponent.ComponentX.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendXComponent.ComponentX.Domain.Repositories;

public interface ImplOrderDetailRepository
{
    Task<IEnumerable<OrderDetail>> ListAsync();
    Task AddAsync(OrderDetail orderDetail);
    Task<OrderDetail> FindByIdAsync(int id);
    void Update(OrderDetail orderDetail);
    void Remove(OrderDetail orderDetail);
    Task<IEnumerable<OrderDetail>> FindByOrderIdAsync(int orderId);
}