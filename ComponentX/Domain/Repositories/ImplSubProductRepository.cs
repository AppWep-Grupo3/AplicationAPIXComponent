using BackendXComponent.ComponentX.Domain.Models;

namespace BackendXComponent.ComponentX.Domain.Repositories;

public interface ImplSubProductRepository
{
    Task<IEnumerable<SubProduct>> ListAsync();

    Task AddAsync(SubProduct subProduct);
    
    Task<SubProduct> FindByIdAsync(int id);
    
    
    
    
    Task<IEnumerable<SubProduct>> FindByProductIdAsync(int productId);
    
    Task<IEnumerable<SubProduct>> FindByNameAsync(string name);
    
    Task<SubProduct> FindByNameOneAsync(string name);
    void Update(SubProduct subProduct);
    void Delete(SubProduct subProduct);
    
    //void RemoveByProductId(int productId);
}