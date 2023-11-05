using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Domain.Repositories;
using BackendXComponent.ComponentX.Domain.Services.Communication;
using BackendXComponent.ComponentX.Domain.Services.Communication.Communication;

namespace BackendXComponent.ComponentX.Services;

public class ProductService: ImplProductService
{
    public readonly ImplProductRepository _productRepository;
    public readonly ImplUnitOfWork _unitOfWork;
    
    public ProductService(ImplProductRepository productRepository, ImplUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<Product>> ListAsync()
    {
      return await _productRepository.ListAsync();
      
    }

    public async Task<ProductResponse> SaveAsync(Product product)
    {
        var existingProduct = await _productRepository.FindByNameAsync(product.Name);
        if (existingProduct != null)
            return new ProductResponse("Product already exists.");
        try
        {
            await _productRepository.AddAsync(product);
            await _unitOfWork.CompleteAsync();
            return new ProductResponse(product);

        }
        catch (Exception e)
        {
            return new ProductResponse($"An error occurred when saving the category: {e.Message}");

        }
        
    }
    
    //Meotodo GetByIdAsync
    public async Task<Product> GetByIdAsync(int id)
    {
        return await _productRepository.FindByIdAsync(id);
    }

    public async Task<ProductResponse> UpdateAsync(int id, Product product)
    {
        var existingProduct = await _productRepository.FindByIdAsync(id);
        if (existingProduct == null)
            return new ProductResponse("Product not found.");
        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.Image= product.Image;
        existingProduct.SpecificDetails = product.SpecificDetails;
        existingProduct.GeneralDetails = product.GeneralDetails;
        try
        {
            _productRepository.Update(existingProduct);
            await _unitOfWork.CompleteAsync();
            return new ProductResponse(existingProduct);
        }
        catch (Exception e)
        {
            return new ProductResponse($"An error occurred when updating the category: {e.Message}");
        }
    }

    public async Task<ProductResponse> DeleteAsync(int id)
    { 
        var existingProduct = await _productRepository.FindByIdAsync(id);
        if (existingProduct == null)
            return new ProductResponse("Product not found.");
        try
        {
            _productRepository.Remove(existingProduct);
            await _unitOfWork.CompleteAsync();
            return new ProductResponse(existingProduct);
        }
        catch (Exception e)
        {
            return new ProductResponse($"An error occurred when deleting the category: {e.Message}");
        }
    }
    
}