using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.Shared.Domain.Services.Communication;

namespace BackendXComponent.ComponentX.Domain.Services.Communication.Communication;

public class CartResponse : BaseResponse<Cart>
{
    public CartResponse(Cart resource) : base(resource)
    {
    }
    
    public CartResponse(string message) : base(message)
    {
    }
}