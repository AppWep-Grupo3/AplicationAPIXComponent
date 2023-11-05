using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Domain.Repositories;
using BackendXComponent.Shared.Persistence.Contexts;
using BackendXComponent.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
namespace BackendXComponent.ComponentX.Persistence.Repositories;

public class CartRepository : BaseRepository, ImplCartRepository // Asegúrate de que la interfaz sea ICartRepository
{
    public CartRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Cart>> ListAsync()
    {
        return await _context.Carts.ToListAsync();
    }
    
    public async Task<IEnumerable<Cart>> GetCartByUserId(int userId)
    {
        return await _context.Carts.Where(c => c.UserId == userId).ToListAsync();
    }

    public async Task AddAsync(Cart cart)
    {
        if (cart == null)
        {
            throw new ArgumentNullException(nameof(cart));
        }

        await _context.Carts.AddAsync(cart);
    }

    public async Task<Cart> FindByIdAsync(int id)
    {
        return await _context.Carts.FindAsync(id);
    }

    public void Update(Cart cart)
    {
        if (cart == null)
        {
            throw new ArgumentNullException(nameof(cart));
        }

        _context.Carts.Update(cart);
    }

    public void Remove(Cart cart)
    {
        if (cart == null)
        {
            throw new ArgumentNullException(nameof(cart));
        }

        _context.Carts.Remove(cart);
    }
}