using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Domain.Repositories;
using BackendXComponent.ComponentX.Domain.Services.Communication;
using BackendXComponent.ComponentX.Domain.Services.Communication.Communication;

namespace BackendXComponent.ComponentX.Services;

public class SubProductService: ImplSubProductService
{
    private readonly ImplProductRepository _productRepository;
    private readonly ImplUnitOfWork _unitOfWork;
    
    private readonly ImplSubProductRepository _subProductRepository;
    
    public SubProductService(ImplProductRepository productRepository, ImplUnitOfWork unitOfWork, ImplSubProductRepository subProductRepository)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _subProductRepository = subProductRepository;
    }
    
    public async Task<IEnumerable<SubProduct>> GetAllAsync()
    {
        return await _subProductRepository.ListAsync();
       
    }

    

    public async Task<SubProductResponse> AddAsync(SubProduct subProduct)
    {
        //Validar productIF
        var existingProduct = await _productRepository.FindByIdAsync(subProduct.ProductId);
        
        if (existingProduct == null) return new SubProductResponse("The PRODUCT GENEREAL doesn't exist");
        
        //Validar Name subproduct
        var existingSubProductWithName = await _subProductRepository.FindByNameOneAsync(subProduct.Name);        
        
        if (existingSubProductWithName != null) return new SubProductResponse("SubProduct name already exists");
        
        var countSubProductByProductId = await _subProductRepository.FindByProductIdAsync(subProduct.ProductId);
        
        if (countSubProductByProductId.Count() >= 5) return new SubProductResponse("The product already has 5 subproducts");
        
        try
        {
            await _subProductRepository.AddAsync(subProduct);
            await _unitOfWork.CompleteAsync();
            return new SubProductResponse(subProduct);
        }
        catch (Exception e)
        {
            return new SubProductResponse($"An error occurred when saving the product: {e.Message}");
        }
    }
    
    //Meotodo GetByIdAsync
    public async Task<SubProduct> GetByIdAsync(int id)
    {
           return await _subProductRepository.FindByIdAsync(id);
           
    }
    
    //Metodo FindByProductIdAsync
    public async Task<IEnumerable<SubProduct>> FindByProductIdAsync(int productId)
    {
        return await _subProductRepository.FindByProductIdAsync(productId);
    }
    
    //Actualizar subProducto
    public async Task<SubProductResponse> Update(int id, SubProduct subProduct)
    {
        var existingSubProduct = await _subProductRepository.FindByIdAsync(id);
        if (existingSubProduct == null)
            return new SubProductResponse("SubProduct not found.");
        //existingSubProduct.Name = subProduct.Name;
        
        //Validad si existe productId
        var existingProduct = await _productRepository.FindByIdAsync(subProduct.ProductId);
        
        if (existingProduct == null) return new SubProductResponse("Invalid Product");
        
        //Modificar tood menos el id
        existingSubProduct.Name = subProduct.Name;
        existingSubProduct.ProductId = subProduct.ProductId;
        existingSubProduct.Price = subProduct.Price;
        existingSubProduct.Specification = subProduct.Specification;
        existingSubProduct.Image = subProduct.Image;
        
        
        
        try
        {
            _subProductRepository.Update(existingSubProduct);
            await _unitOfWork.CompleteAsync();
            return new SubProductResponse(existingSubProduct);
        }
        catch (Exception e)
        {
            return new SubProductResponse($"An error occurred when updating the subProduct: {e.Message}");
        }
        
        
    }
    //Elimianr
    public async Task<SubProductResponse> Delete(int id)
    {
        var existingSubProduct = await _subProductRepository.FindByIdAsync(id);
        if (existingSubProduct == null)
            return new SubProductResponse("SubProduct not found.");
        try
        {
            _subProductRepository.Delete(existingSubProduct);
            await _unitOfWork.CompleteAsync();
            return new SubProductResponse(existingSubProduct);
        }
        catch (Exception e)
        {
            return new SubProductResponse($"An error occurred when deleting the subProduct: {e.Message}");
        }
    } 
}