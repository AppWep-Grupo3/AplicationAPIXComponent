using BackendXComponent.Security.Domain.Models;
using BackendXComponent.Security.Domain.Repositories;
using BackendXComponent.Shared.Persistence.Contexts;
using BackendXComponent.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendXComponent.Security.Persistence.Repositories;

public class UserRepository:BaseRepository, ImplUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _context.Users.ToListAsync();
    }
    
    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }
    
    public async Task<User> FindByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }
    
    //public async Task<User> FindByUsernameAsync(string username)
    //{
    //    return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
    //}

    
    
    public async Task<User> FindByEmailAsync(string email)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<User>> GetUserByEmailAndPassword(string email, string password)
    {
        return await _context.Users
            .Where(p => p.Email == email && p.Password == password)
            .ToListAsync();
        
    }   
    
    public bool ExistsByEmail(string email)
    {
        return _context.Users.Any(p => p.Email == email);
    }
    

    
    public User FindById(int id)
    {
        return _context.Users.Find(id);
    }
    
    public void Update(User user)
    {
        _context.Users.Update(user);
    }
    
    public void Remove(User user)
    {
        _context.Users.Remove(user);
    }
    
}