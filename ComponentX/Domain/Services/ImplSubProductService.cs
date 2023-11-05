using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Domain.Services.Communication.Communication;

namespace BackendXComponent.ComponentX.Domain.Services.Communication;

public interface ImplSubProductService
{
       Task<IEnumerable<SubProduct>> GetAllAsync();
       Task<SubProductResponse> AddAsync(SubProduct subProduct);
       
       Task<SubProduct> GetByIdAsync(int id);
    
        

        Task<IEnumerable<SubProduct>> FindByProductIdAsync(int productId);
        
        Task<SubProductResponse> Update(int subProductId, SubProduct subProduct);
        
        Task<SubProductResponse> Delete(int subProductId);
   
}