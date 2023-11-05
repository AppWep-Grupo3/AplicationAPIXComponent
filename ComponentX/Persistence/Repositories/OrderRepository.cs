using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Domain.Repositories;
using BackendXComponent.Shared.Persistence.Contexts;
using BackendXComponent.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
namespace BackendXComponent.ComponentX.Persistence.Repositories;

public class OrderRepository : BaseRepository, ImplOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Order>> ListAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    public async Task<Order> FindByIdAsync(int id)
    {
        return await _context.Orders.FindAsync(id);
    }
    
    public async Task<Order> FindUserIdAsync(int userId)
    {
        return await _context.Orders
            .FirstOrDefaultAsync(p => p.UserId == userId);
    }
    
    public void Update(Order order)
    {
        _context.Orders.Update(order);
    }
    
    public async Task<Order> FindByDateAsync(DateTime date)
    {
        return await _context.Orders
            .FirstOrDefaultAsync(p => p.Date == date);
    }

    public void Delete(Order order)
    {
        _context.Orders.Remove(order);
    }
}