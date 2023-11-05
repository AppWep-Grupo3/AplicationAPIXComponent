using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Domain.Repositories;
using BackendXComponent.Shared.Persistence.Contexts;
using BackendXComponent.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
namespace BackendXComponent.ComponentX.Persistence.Repositories;

public class UserRepository : BaseRepository, ImplUserRespository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _context.Users.ToListAsync();
    }
    
    public async Task<IEnumerable<User>> GetUserByEmailAndPassword(string email, string password)
    {
        return await _context.Users
            .Where(p => p.Email == email && p.Password == password)
            .ToListAsync();
    }
    
    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }
    
    public async Task<User> FindByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }
    
    public void Update(User user)
    {
        _context.Users.Update(user);
    }
    
    public async Task<User> FindByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(p => p.Email == email);
    }
    
    public void Delete(User user)
    {
        _context.Users.Remove(user);
    }
}