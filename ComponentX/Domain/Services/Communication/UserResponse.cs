using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.Shared.Domain.Services.Communication;

namespace BackendXComponent.ComponentX.Domain.Services.Communication.Communication;

public class UserResponse : BaseResponse<User>
{
    public UserResponse(User resource) : base(resource)
    {
    }
    
    public UserResponse(string message) : base(message)
    {
    }
}