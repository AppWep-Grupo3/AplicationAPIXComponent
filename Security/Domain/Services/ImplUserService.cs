using BackendXComponent.Security.Domain.Models;
using BackendXComponent.Security.Domain.Services.Communication;

namespace BackendXComponent.Security.Domain.Services;

public interface ImplUserService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    Task<IEnumerable<User>> ListAsync();
    Task<User> GetByIdAsync(int id);
    
    
    Task<IEnumerable<User>> GetUserByEmailAndPassword(string email, string password);
    
    
    Task RegisterAsync(RegisterRequest model);
    Task UpdateAsync(int id, UpdateRequest model);
    Task DeleteAsync(int id);
}