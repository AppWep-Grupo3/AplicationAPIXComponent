using BackendXComponent.ComponentX.Domain.Models;

namespace BackendXComponent.ComponentX.Domain.Repositories;

public interface ImplProductRepository
{
    Task<IEnumerable<Product>> ListAsync();
    
    Task AddAsync(Product product);
    
    Task<Product> FindByNameAsync(string name);
    
    Task<Product> FindByIdAsync(int id);
    
    void Update(Product product);
    
    void Remove(Product product);
}