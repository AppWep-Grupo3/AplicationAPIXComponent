using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Domain.Services.Communication.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendXComponent.ComponentX.Domain.Services.Communication;

public interface ImpOrderDetailService
{
    Task<IEnumerable<OrderDetail>> ListAsync();
    Task<OrderDetailResponse> SaveAsync(OrderDetail orderDetail);
    Task<OrderDetailResponse> UpdateAsync(int id, OrderDetail orderDetail);
    Task<OrderDetailResponse> DeleteAsync(int id);
}