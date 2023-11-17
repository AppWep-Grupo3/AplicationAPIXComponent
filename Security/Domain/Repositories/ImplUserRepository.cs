using BackendXComponent.Security.Domain.Models;

namespace BackendXComponent.Security.Domain.Repositories;

public interface ImplUserRepository
{
    Task<IEnumerable<User>> ListAsync();
    Task AddAsync(User user);
    Task<User> FindByIdAsync(int id);
    //Task<User> FindByUsernameAsync(string username);
  
    
    Task<User> FindByEmailAsync(string email);
    Task<IEnumerable<User>> GetUserByEmailAndPassword(string email, string password);    
    
   public bool ExistsByEmail(string email);
    
    User FindById(int id);
    void Update(User user);
    void Remove(User user);
}