using BackendXComponent.Security.Domain.Models;

namespace BackendXComponent.Security.Authorization.Handlers.Interfaces;

public interface IJwtHandler
{
    
    public string GenerateToken(User user);
    public int? ValidateToken(string token);
}