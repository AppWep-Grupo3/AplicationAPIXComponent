using AutoMapper;
using BackendXComponent.Security.Domain.Models;
using BackendXComponent.Security.Domain.Services.Communication;
using BackendXComponent.Security.Resources.Response;

namespace BackendXComponent.Security.Mapping;

public class ModelToResourceProfile: Profile
{
    
    //constructo
    public ModelToResourceProfile()
    {
        CreateMap<User, AuthenticateResponse>();
        CreateMap<User, UserResponseDto>();
    }
}