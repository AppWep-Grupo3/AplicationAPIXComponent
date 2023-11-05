namespace BackendXComponent.Shared.Domain.Services.Communication;

public abstract class BaseResponse<T>
{
    public bool Success { get; set; }
    
    public string Message { get; set; }
    
    public T Resource { get; set; }

    protected BaseResponse(T resource)
    {   //respuesta exitosa
        Success = true;
        Resource = resource;
        Message = string.Empty;
    }


    protected BaseResponse(string message)
    {
        //respuesta fallida
        Success = false;
        Message = message;
        Resource = default;
    }
}