using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Domain.Repositories;
using BackendXComponent.Shared.Persistence.Contexts;
using BackendXComponent.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
namespace BackendXComponent.ComponentX.Persistence.Repositories;

public class OrderDetailRepository : BaseRepository, ImplOrderDetailRepository
{
    public OrderDetailRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<OrderDetail>> ListAsync()
    {
        return await _context.OrderDetails.ToListAsync();
    }

    public async Task AddAsync(OrderDetail orderDetail)
    {
        await _context.OrderDetails.AddAsync(orderDetail);
    }

    public async Task<OrderDetail> FindByIdAsync(int id)
    {
        return await _context.OrderDetails.FindAsync(id);
    }

    public async Task<IEnumerable<OrderDetail>> FindByOrderIdAsync(int orderId)
    {
        return await _context.OrderDetails
            .Where(od => od.OrderId == orderId)
            .ToListAsync();
    }

    public void Update(OrderDetail orderDetail)
    {
        _context.OrderDetails.Update(orderDetail);
    }

    public void Remove(OrderDetail orderDetail)
    {
        _context.OrderDetails.Remove(orderDetail);
    }
}