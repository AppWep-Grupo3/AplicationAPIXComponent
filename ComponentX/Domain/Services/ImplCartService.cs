using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Domain.Services.Communication.Communication;
using BackendXComponent.ComponentX.Resources;

namespace BackendXComponent.ComponentX.Domain.Services.Communication;

public interface ImplCartService
{
    Task<IEnumerable<Cart>> ListAsync();

    Task<IEnumerable<Cart>> GetCartByUserId(int userId);


    Task<CartResponse> UpdateAsync(int id, UpdateQuantityAndPriceResource cart);

    Task<CartResponse> SaveAsync(Cart cart);


    Task<CartResponse> DeleteAsync(int id, DeleteFromCarritoResource resource);
}