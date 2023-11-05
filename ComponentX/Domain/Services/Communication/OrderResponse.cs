using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.Shared.Domain.Services.Communication;

namespace BackendXComponent.ComponentX.Domain.Services.Communication.Communication;

public class OrderResponse : BaseResponse<Order>
{
    public OrderResponse(Order resource) : base(resource)
    {
    }

    public OrderResponse(string message) : base(message)
    {
    }
}