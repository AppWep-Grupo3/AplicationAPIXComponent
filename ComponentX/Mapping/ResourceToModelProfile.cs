using AutoMapper;
using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Resources;

namespace BackendXComponent.ComponentX.Mapping;

public class ResourceToModelProfile: Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveProductResource, Product>();
        CreateMap<SaveSubProductResource, SubProduct>();
        CreateMap<SaveUserResource, User>();
        CreateMap<UserResource, User>();
        CreateMap<OrderDetail, OrderDetail>();
        CreateMap<SaveCartResource, Cart>();
    }
    
}