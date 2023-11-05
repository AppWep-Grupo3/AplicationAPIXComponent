using BackendXComponent.ComponentX.Domain.Models;

namespace BackendXComponent.ComponentX.Domain.Repositories;

public interface ImplUserRespository
{
    Task<IEnumerable<User>> ListAsync();
    
    Task<IEnumerable<User>> GetUserByEmailAndPassword(string email, string password);

    Task AddAsync(User user);
    
    Task<User> FindByIdAsync(int id);
    
    Task<User> FindByEmailAsync(string email);
    
    void Update(User user);
    void Delete(User user);
}