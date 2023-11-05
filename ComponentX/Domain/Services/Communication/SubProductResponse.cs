using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.Shared.Domain.Services.Communication;

namespace BackendXComponent.ComponentX.Domain.Services.Communication.Communication;

public class SubProductResponse: BaseResponse<SubProduct>
{
    //Respuesta exitosa
    public SubProductResponse(SubProduct resource) : base(resource)
    {
    }

    //Respuesta fallida
    public SubProductResponse(string message) : base(message)
    {
    }
    
}