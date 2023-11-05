using AutoMapper;
using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Resources;

namespace BackendXComponent.ComponentX.Mapping;

public class ModelToResourceProfile: Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Product, ProductResource>();
        CreateMap<SubProduct, SubProductResource>();
        CreateMap<User, UserResource>();
        CreateMap<OrderDetail, OrderDetail>();
        CreateMap<Cart,  CartResource>();
        
    }
}