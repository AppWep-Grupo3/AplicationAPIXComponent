using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.Shared.Domain.Services.Communication;

namespace BackendXComponent.ComponentX.Domain.Services.Communication.Communication;

public class OrderDetailResponse : BaseResponse<OrderDetail>
{
    public OrderDetailResponse(OrderDetail resource) : base(resource)
    {
    }

    public OrderDetailResponse(string message) : base(message)
    {
    }
}