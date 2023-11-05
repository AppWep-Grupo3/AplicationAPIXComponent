using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.Shared.Domain.Services.Communication;

namespace BackendXComponent.ComponentX.Domain.Services.Communication.Communication;

public class ProductResponse: BaseResponse<Product>
{
    //Respuesta exitosa
    public ProductResponse(Product resource) : base(resource)
    {
    }

    //Respuesta fallida
    public ProductResponse(string message) : base(message)
    {
    }
    
}
